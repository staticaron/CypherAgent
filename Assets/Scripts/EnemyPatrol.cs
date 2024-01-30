using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
	[SerializeField] List<Transform> positions;

	[SerializeField] float moveSpeed;

	private int index = 0;

	private void Update()
	{
		Vector3 positionToSeek = positions[index].position;

		Vector3 delta = positionToSeek - transform.position;
		delta.y = 0;

		if (Mathf.Sqrt(Vector3.SqrMagnitude(delta)) > 0.1)
		{
			transform.Translate(delta.normalized * moveSpeed);
		}
		else
		{
			index = index + 1 < positions.Count ? index + 1 : 0;
		}
	}
}
