using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Cypher))]
[RequireComponent(typeof(CypherAbilityController))]
public class PlayerInputManager : MonoBehaviour
{
    private CypherAbilityController abilityController;

    [SerializeField] CypherMainChannelSO cypherMainChannelSO;

    private void Awake()
    {
        abilityController = GetComponent<CypherAbilityController>();
    }

    public void OnAbility(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Performed) return;

        if (cypherMainChannelSO.RaiseGetState() == AgentState.ABILITY)
        {
            abilityController.StopAbility();
            cypherMainChannelSO.RaiseSetState(AgentState.NONE);
        }
        else if (cypherMainChannelSO.RaiseGetState() == AgentState.NONE)
        {
            abilityController.StartAbility();
            cypherMainChannelSO.RaiseSetState(AgentState.ABILITY);
        }
    }

    public void OnPrimary(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Performed) return;

        if (cypherMainChannelSO.RaiseGetState() == AgentState.ABILITY)
        {
            abilityController.Primary();
        }
        else if (cypherMainChannelSO.RaiseGetState() == AgentState.NONE)
        {

        }
    }

    public void OnSecondary(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Performed) return;

        if (cypherMainChannelSO.RaiseGetState() == AgentState.ABILITY)
        {
            abilityController.Secondary();
        }
        else if (cypherMainChannelSO.RaiseGetState() == AgentState.NONE)
        {

        }
    }

    public void OnX(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Performed) return;

        abilityController.RecallDecon();
    }
}
