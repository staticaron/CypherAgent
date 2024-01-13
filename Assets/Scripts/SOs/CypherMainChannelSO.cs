using UnityEngine;

[CreateAssetMenu(fileName = "CypherMainChannelSO", menuName = "CypherAbility/CypherMainChannelSO", order = 0)]
public class CypherMainChannelSO : ScriptableObject
{
    public delegate AgentState getState();
    public event getState E_GetState;

    public delegate void setState(AgentState stateToSet);
    public event setState E_SetState;


    public AgentState? RaiseGetState()
    {
        if (E_GetState != null) return E_GetState.Invoke();
        else
        {
            Debug.LogWarning("No one is there to perform GetState!");
            return null;
        }
    }

    public void RaiseSetState(AgentState stateToSet)
    {
        if (E_SetState != null) E_SetState.Invoke(stateToSet);
        else
        {
            Debug.LogWarning("No one is there to perform GetState!");
        }
    }

    public delegate bool deconDeployed();
    public event deconDeployed E_DeconDeploymentStatus;

    public delegate void setDeconStatus(bool place);
    public event setDeconStatus E_SetDeconStatus;

    public bool? RaiseDeconDeploymentStatus()
    {
        if (E_DeconDeploymentStatus != null) return E_DeconDeploymentStatus.Invoke();
        else
        {
            Debug.LogWarning("No one is there to perform DeconDeploymentStatus!");
            return null;
        }
    }

    public void RaiseSetDeconState(bool placed)
    {
        if (E_SetDeconStatus != null) E_SetDeconStatus.Invoke(placed);
        else
        {
            Debug.LogWarning("No one is there to perform SetDeconState!");
        }
    }

    public delegate void SetHandState(bool enable);
    public event SetHandState E_SetHandState;

    public void RaiseSetHandState(bool enable)
    {
        if (E_SetHandState != null) E_SetHandState.Invoke(enable);
        else
        {
            Debug.LogWarning("No one is there to perform SetDeconState!");
        }
    }
}