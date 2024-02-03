using Cypher;
using Enums;
using SO;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InputManagers
{

	[RequireComponent(typeof(Cypher.Cypher))]
	[RequireComponent(typeof(CypherAbilityController))]
	[RequireComponent(typeof(CypherAbility2Controller))]
	[RequireComponent(typeof(UltimateController))]

	public class AgentInputManager : MonoBehaviour
	{
		private CypherAbilityController abilityController;
		private CypherAbility2Controller ability2Controller;
		private CypherAbility3Controller ability3Controller;
		private UltimateController ultimateController;

		[SerializeField] CypherMainChannelSO cypherMainChannelSO;
		[SerializeField] UIChannelSO uiChannelSO;

		private void Awake()
		{
			abilityController = GetComponent<CypherAbilityController>();
			ability2Controller = GetComponent<CypherAbility2Controller>();
			ability3Controller = GetComponent<CypherAbility3Controller>();
			ultimateController = GetComponent<UltimateController>();
		}

		public void OnAbility(InputAction.CallbackContext context)
		{
			if (context.phase != InputActionPhase.Performed) return;

			if (cypherMainChannelSO.RaiseGetState() == AgentState.ABILITY)
			{
				abilityController.EndAbility();
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
			else if (cypherMainChannelSO.RaiseGetState() == AgentState.ABILITY2)
			{
				ability2Controller.Primary();
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
			else if (cypherMainChannelSO.RaiseGetState() == AgentState.ABILITY2)
			{
				ability2Controller.Secondary();
			}
			else if (cypherMainChannelSO.RaiseGetState() == AgentState.NONE)
			{

			}
		}

		public void OnX(InputAction.CallbackContext context)
		{
			if (context.phase != InputActionPhase.Performed) return;
			if (cypherMainChannelSO.RaiseGetState() == AgentState.NONE) abilityController.RecallDecon();
		}

		public void OnC(InputAction.CallbackContext context)
		{
			if (context.phase != InputActionPhase.Performed) return;

			if (cypherMainChannelSO.RaiseGetState() == AgentState.NONE) ability2Controller.StartAbility();
			else if (cypherMainChannelSO.RaiseGetState() == AgentState.ABILITY2) ability2Controller.EndAbility();
		}

		public void OnZ(InputAction.CallbackContext context)
		{
			if (context.phase != InputActionPhase.Performed) return;

			if (cypherMainChannelSO.RaiseGetState() == AgentState.NONE)
			{
				ultimateController.StartAbility();
			}
			else if (cypherMainChannelSO.RaiseGetState() == AgentState.ULTIMATE)
			{
				ultimateController.EndAbility();
			}
		}

		public void OnF(InputAction.CallbackContext context)
		{
			if (context.phase != InputActionPhase.Performed) return;

			if (cypherMainChannelSO.RaiseGetState() == AgentState.NONE) ability3Controller.StartAbility();
			else if (cypherMainChannelSO.RaiseGetState() == AgentState.ABILITY3) ability3Controller.EndAbility();
		}
	}
}