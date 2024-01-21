using UnityEngine;

namespace Cypher.Entities
{
	public class CageNade : MonoBehaviour
	{
		private const string GroundTag = "Ground";

		[SerializeField] GameObject cageSmokeObj;
		[SerializeField] float launchForce;

		[SerializeField] Transform launchPoint;
		[SerializeField] Transform directionPoint;

		private Rigidbody rb;

		private void Awake()
		{
			rb = GetComponent<Rigidbody>();
		}

		public void Launch()
		{
			transform.position = launchPoint.position;
			rb.velocity = Vector3.zero;
			rb.AddForce(directionPoint.forward * launchForce);
		}

		private void OnCollisionEnter(Collision collision)
		{
			if (collision.collider.CompareTag(GroundTag))
			{
				GameObject instance = Instantiate(cageSmokeObj, transform.position, Quaternion.identity);
				Destroy(instance, 5);

				gameObject.SetActive(false);
			}
		}
	}
}