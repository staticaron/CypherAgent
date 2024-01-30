using SO;
using System.Collections.Generic;
using UnityEngine;

public class SnapToEnemy : MonoBehaviour
{
	public static SnapToEnemy Instance { get; private set; }

	// Called whenever the currently snapped obj is changed
	public delegate void SnapUpdate(Vector3? newPosition);
	public static event SnapUpdate E_SnapUpdate;

	[Header("Snapping Options")]
	[SerializeField] float maxReach;
	[SerializeField] LayerMask enemyLayer;

	[SerializeField] float maxDistanceToSnap;

	[SerializeField] Transform snappedEnemy;
	private Vector2? snappedEnemyScreenPosition;

	private Vector2? SnappedEnemyScreenPosition
	{
		get { return snappedEnemyScreenPosition; }
		set
		{
			if (value == snappedEnemyScreenPosition) return;

			snappedEnemyScreenPosition = value;
			E_SnapUpdate.Invoke(value);
		}
	}

	[SerializeField] AgentManagerChannelSO agentManagerChannelSO;

	private Camera mainCam;

	private void Awake()
	{
		mainCam = Camera.main;

		if (Instance == null) Instance = this;
		else Destroy(gameObject);
	}

	private void LateUpdate()
	{
		Transform playerTransform = agentManagerChannelSO.RaiseGetPlayer().GetTransform();

		Collider[] enemiesInRange = Physics.OverlapSphere(playerTransform.position, maxReach, enemyLayer);

		List<Transform> enemiesInFrustum = new List<Transform>();
		List<Vector2> screenPositions = new List<Vector2>();

		foreach (Collider enemy in enemiesInRange)
		{
			if (isInCameraFrustum(enemy.GetComponent<Renderer>()))
			{
				Vector3 rayDirection = (enemy.transform.position - mainCam.transform.position).normalized;
				Ray ray = new Ray(mainCam.transform.position, rayDirection);

				if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity))
				{
					// Ignore if not in LOS.
					LayerMask currentLayer = hitInfo.collider.gameObject.layer;
					if (((enemyLayer >> currentLayer) & 1) != 1) continue;

					enemiesInFrustum.Add(enemy.transform);
					screenPositions.Add(mainCam.WorldToScreenPoint(enemy.transform.position));
				}
			}
		}

		int index = -1;
		float closestDistance = maxDistanceToSnap;

		Vector2 crossHairScreenPosition = new Vector2(mainCam.pixelWidth * 0.5f, mainCam.pixelHeight * 0.5f);

		for (int x = 0; x < screenPositions.Count; x++)
		{
			float distance = Mathf.Sqrt(Vector2.SqrMagnitude(screenPositions[x] - crossHairScreenPosition));

			if (distance < closestDistance)
			{
				closestDistance = distance;
				index = x;
			}
		}

		snappedEnemy = index == -1 ? null : enemiesInFrustum[index];
		SnappedEnemyScreenPosition = index == -1 ? null : screenPositions[index];
	}

	private bool isInCameraFrustum(Renderer _renderer)
	{
		Plane[] frustumPlanes = GeometryUtility.CalculateFrustumPlanes(mainCam);

		return GeometryUtility.TestPlanesAABB(frustumPlanes, _renderer.bounds);
	}

	public Transform GetSnappedEnemy()
	{
		return snappedEnemy;
	}

	public Vector2? GetSnappedEnemyScreenPos()
	{
		return snappedEnemyScreenPosition;
	}
}
