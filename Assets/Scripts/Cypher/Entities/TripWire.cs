using Core;
using UnityEngine;

namespace Cypher.Entities
{
	public class TripWire : MonoBehaviour
	{
		private const string EnemyTag = "Enemy";

		[SerializeField] float maxRayCheckDistance;
		[SerializeField] float highlightTime;
		[SerializeField] LayerMask tripWireDetectionLayer;

		[Header("References")]
		[SerializeField] LineRenderer deconLineRenderer;

		private bool tripped = false;

		private void Update()
		{
			if (tripped) return;

			Ray ray = new Ray(transform.position, transform.forward);

			RaycastHit hitInfo;

			if (Physics.Raycast(ray, out hitInfo, maxRayCheckDistance, tripWireDetectionLayer))
			{
				if (deconLineRenderer.positionCount != 2)
				{
					deconLineRenderer.positionCount = 2;
					deconLineRenderer.SetPosition(0, transform.position);
					deconLineRenderer.SetPosition(1, hitInfo.point);
				}

				if (hitInfo.collider.CompareTag(EnemyTag))
				{
					hitInfo.collider.GetComponent<Enemy>().EnableHighlights(highlightTime);
					tripped = true;
					gameObject.SetActive(false);
				}
			}
		}
	}
}