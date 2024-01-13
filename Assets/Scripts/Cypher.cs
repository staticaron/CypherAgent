using UnityEngine;
using UnityEngine.Rendering.Universal;

public enum AgentState
{
    ULTIMATE,
    ABILITY,
    NONE
}

public class Cypher : MonoBehaviour
{
    [SerializeField] AgentState currentState = AgentState.NONE;

    [SerializeField] bool deconInPlace = false;

    [SerializeField] Camera gunCam;

    [Header("SOs")]
    [SerializeField] CypherMainChannelSO cypherMainChannelSO;

    private Camera mainCam;

    private void Awake()
    {
        mainCam = Camera.main;

        mainCam.GetUniversalAdditionalCameraData().cameraStack.Add(gunCam);
    }

    private void OnEnable()
    {
        cypherMainChannelSO.E_GetState += GetState;
        cypherMainChannelSO.E_SetState += SetStatus;
        cypherMainChannelSO.E_DeconDeploymentStatus += GetDeconStatus;
        cypherMainChannelSO.E_SetDeconStatus += SetDeconStatus;
        cypherMainChannelSO.E_SetHandState += SetHandState;
    }

    private void OnDisable()
    {
        cypherMainChannelSO.E_GetState -= GetState;
        cypherMainChannelSO.E_SetState -= SetStatus;
        cypherMainChannelSO.E_DeconDeploymentStatus -= GetDeconStatus;
        cypherMainChannelSO.E_SetDeconStatus -= SetDeconStatus;
        cypherMainChannelSO.E_SetHandState -= SetHandState;
    }

    private AgentState GetState()
    {
        return currentState;
    }

    private void SetStatus(AgentState _state)
    {
        currentState = _state;
    }

    private bool GetDeconStatus()
    {
        return deconInPlace;
    }

    private void SetDeconStatus(bool _placed)
    {
        deconInPlace = _placed;
    }

    private void SetHandState(bool enable)
    {
        bool gunCamIsActive = mainCam.GetUniversalAdditionalCameraData().cameraStack.Contains(gunCam);

        if (!gunCamIsActive && enable)
        {
            mainCam.GetUniversalAdditionalCameraData().cameraStack.Add(gunCam);
            CrosshairManager.Instance.ResetCrosshair();
        }
        else if (gunCamIsActive && !enable)
        {
            mainCam.GetUniversalAdditionalCameraData().cameraStack.Remove(gunCam);
            CrosshairManager.Instance.SetCypherCrosshair();
        }
    }
}
