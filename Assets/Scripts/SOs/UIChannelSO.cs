using Enums;
using UnityEngine;

namespace SO
{
	[CreateAssetMenu(fileName = "UIChannelSO", menuName = "CypherAbility/UIChannelSO", order = 0)]
	public class UIChannelSO : ScriptableObject
	{
		public delegate void SetAbilityState(AbilityType type, IconState state);
		public event SetAbilityState E_SetAbilityState;

		public void RaiseSetAbilityState(AbilityType _type, IconState _state)
		{
			if (E_SetAbilityState != null) E_SetAbilityState.Invoke(_type, _state);
			else
			{
				Debug.LogError("No one is available to run SetAbilityState");
			}
		}

		public delegate void ResetIconSize();
		public event ResetIconSize E_ResetIconSize;

		public void RaiseResetIconSize()
		{
			if (E_ResetIconSize != null) E_ResetIconSize.Invoke();
			else
			{
				Debug.LogError("No one is available to run ResetIconState");
			}
		}
	}
}