using Interfaces;
using UnityEngine;

namespace SO
{
	[CreateAssetMenu(fileName = "AgentManagerChannelSO", menuName = "CypherAbility/AgentManagerChannelSO", order = 0)]
	public class AgentManagerChannelSO : ScriptableObject
	{
		public delegate void HighlightEnemies(float highlightTime);
		public event HighlightEnemies E_HighlightEnemies;

		public void RaiseHighlightEnemies(float highlightTime)
		{
			if (E_HighlightEnemies != null) E_HighlightEnemies.Invoke(highlightTime);
			else
			{
				Debug.LogWarning("No one is available to run HighlightEnemies");
			}
		}

		public delegate void CancelHighlightEnemies();
		public event CancelHighlightEnemies E_CancelHighlightEnemies;

		public void RaiseCancelHighlightEnemies()
		{
			if (E_CancelHighlightEnemies != null) E_CancelHighlightEnemies.Invoke();
			else
			{
				Debug.LogWarning("No one is available to run CancelHighlightEnemies");
			}
		}

		public delegate void SetPlayer(AgentBase player);
		public event SetPlayer E_SetPlayer;

		public void RaiseSetPlayer(AgentBase player)
		{
			if (E_SetPlayer != null) E_SetPlayer.Invoke(player);
			else
			{
				Debug.LogWarning("No one is available to run SetPlayer");
			}
		}

		public delegate AgentBase GetPlayer();
		public event GetPlayer E_GetPlayer;

		public AgentBase RaiseGetPlayer()
		{
			if (E_GetPlayer != null) return E_GetPlayer.Invoke();
			else
			{
				Debug.LogWarning("No one is available to run GetPlayer");
				return null;
			}
		}
	}
}