using UnityEngine;

public class TripWire : MonoBehaviour
{
    private const string EnemyTag = "Enemy";

    [SerializeField] float maxRayDistance;
    [SerializeField] LayerMask tripWireDetectionLayer;

    [SerializeField] LineRenderer deconLineRenderer;

    private bool tripped = false;

    private void Update()
    {
        if (tripped) return;

        Ray ray = new Ray(transform.position, transform.forward);

        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, maxRayDistance, tripWireDetectionLayer))
        {
            if (deconLineRenderer.positionCount != 2)
            {
                deconLineRenderer.positionCount = 2;
                deconLineRenderer.SetPosition(0, transform.position);
                deconLineRenderer.SetPosition(1, hitInfo.point);
            }

            if (hitInfo.collider.CompareTag(EnemyTag))
            {
                Debug.Log("TRIP WIRE TRIGGERED!");
                tripped = true;
                gameObject.SetActive(false);
            }
        }
    }
}
