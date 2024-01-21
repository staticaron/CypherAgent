using UnityEngine;

namespace Cypher
{
    public class CypherAbility2TemporaryDecon : MonoBehaviour
    {
        [SerializeField] float maxRayDistance;
        [SerializeField] LayerMask wallLayer;

        [SerializeField] LineRenderer deconLineRenderer;

        private void Update()
        {
            Ray ray = new Ray(transform.position, transform.forward);

            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, maxRayDistance, wallLayer))
            {
                deconLineRenderer.positionCount = 2;
                deconLineRenderer.SetPosition(0, transform.position);
                deconLineRenderer.SetPosition(1, hitInfo.point);
            }
            else
            {
                deconLineRenderer.positionCount = 0;
            }
        }
    }
}