using UnityEngine;

[RequireComponent(typeof(Cypher))]
public class CypherAbilityController : MonoBehaviour
{
    [SerializeField] LayerMask wallLayer;

    [SerializeField] GameObject actualDeconObj;
    [SerializeField] GameObject temporaryDeconObj;

    [SerializeField] Transform propsHolder;

    [SerializeField] bool isInRange = false;

    [SerializeField] bool active = false;

    [SerializeField] CypherCamController activeDecon = null;

    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] Transform gunStartingPoint;

    [SerializeField] CypherMainChannelSO cypherMainChannelSO;

    private Transform mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main.transform;
    }

    private void LateUpdate()
    {
        if (!active) return;

        Ray ray = new Ray(mainCamera.position, mainCamera.forward);

        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, 10, wallLayer))
        {
            isInRange = true;

            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, gunStartingPoint.position);
            lineRenderer.SetPosition(1, hitInfo.point);

            // Position the decon
            if (!temporaryDeconObj.activeInHierarchy) temporaryDeconObj.SetActive(true);
            temporaryDeconObj.transform.position = hitInfo.point;
            temporaryDeconObj.transform.forward = hitInfo.normal;
        }
        else
        {
            isInRange = false;

            temporaryDeconObj.SetActive(false);

            lineRenderer.positionCount = 0;
        }
    }

    public void Primary()
    {
        if (!active) return;
        if (!isInRange) return;

        SpawnDecon();
        StopAbility();
    }

    public void Secondary()
    {
        StopAbility();
    }

    public void StartAbility()
    {
        if (activeDecon)
        {
            activeDecon.StartDecon();
            cypherMainChannelSO.RaiseSetHandState(false);
            return;
        }

        active = true;

        cypherMainChannelSO.RaiseSetState(AgentState.ABILITY);
    }

    public void StopAbility()
    {
        if (activeDecon)
        {
            activeDecon.StopDecon();
            cypherMainChannelSO.RaiseSetHandState(true);
        }

        active = false;

        if (temporaryDeconObj.activeInHierarchy) temporaryDeconObj.SetActive(false);

        lineRenderer.positionCount = 0;

        cypherMainChannelSO.RaiseSetState(AgentState.NONE);
    }

    private void SpawnDecon()
    {
        GameObject instance = Instantiate(actualDeconObj, temporaryDeconObj.transform.position, temporaryDeconObj.transform.rotation);
        instance.transform.parent = propsHolder;

        activeDecon = instance.GetComponent<CypherCamController>();
    }

    public void RecallDecon()
    {
        if (activeDecon)
        {
            Destroy(activeDecon.gameObject);
            activeDecon = null;
        }
    }
}
