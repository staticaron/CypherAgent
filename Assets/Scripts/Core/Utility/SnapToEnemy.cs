using SO;
using System.Collections.Generic;
using UnityEngine;

public class SnapToEnemy : MonoBehaviour
{
	[Header("Snapping Options")]
	[SerializeField] float maxReach;
	[SerializeField] LayerMask enemyLayer;

	[SerializeField] float maxDistanceToSnap;

	[SerializeField] Transform snappedEnemy;
	[SerializeField] Vector2? snappedEnemyScreenPosition;

	[SerializeField] AgentManagerChannelSO agentManagerChannelSO;

	private Camera mainCam;

	public static SnapToEnemy Instance { get; private set; }

	private void Awake()
	{
		mainCam = Camera.main;

		if (Instance == null) Instance = this;
		else Destroy(gameObject);
	}

	private void Update()
	{
		Transform playerTransform = agentManagerChannelSO.RaiseGetPlayer().GetTransform();

		Collider[] enemiesInRange = Physics.OverlapSphere(playerTransform.position, maxReach, enemyLayer);

		List<Transform> enemiesInFrustum = new List<Transform>();

		List<Vector2> screenPositions = new List<Vector2>();

		foreach (Collider enemy in enemiesInRange)
		{
			if (isInCameraFrustum(enemy.GetComponent<Renderer>()))
			{
				enemiesInFrustum.Add(enemy.transform);
				screenPositions.Add(mainCam.WorldToScreenPoint(enemy.transform.position));
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
		snappedEnemyScreenPosition = index == -1 ? null : screenPositions[index];
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
