using Core;
using SO;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
	public class AgentManager : MonoBehaviour
	{
		[SerializeField] AgentManagerChannelSO agentManagerChannelSO;

		[SerializeField] List<Enemy> enemyContainer;
		[SerializeField] List<Teammate> teammateContainer;

		private void OnEnable()
		{
			agentManagerChannelSO.E_HighlightEnemies += HighlightEnemies;
			agentManagerChannelSO.E_CancelHighlightEnemies += CancelHighlightEnemies;
		}

		private void OnDisable()
		{
			agentManagerChannelSO.E_HighlightEnemies -= HighlightEnemies;
			agentManagerChannelSO.E_CancelHighlightEnemies -= CancelHighlightEnemies;

		}

		private void AddEnemy()
		{

		}

		private void RemoveEnemy()
		{

		}

		private void HighlightEnemies(float highlightTime)
		{
			foreach (Enemy e in enemyContainer)
			{
				e.EnableHighlights(highlightTime);
			}
		}

		private void CancelHighlightEnemies()
		{
			foreach (Enemy e in enemyContainer)
			{
				e.DisableHighlights();
			}
		}
	}
}