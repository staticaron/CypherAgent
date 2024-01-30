using Core;
using Interfaces;
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

		[SerializeField] AgentBase player;

		private void OnEnable()
		{
			agentManagerChannelSO.E_HighlightEnemies += HighlightEnemies;
			agentManagerChannelSO.E_CancelHighlightEnemies += CancelHighlightEnemies;
			agentManagerChannelSO.E_SetPlayer += SetPlayer;
			agentManagerChannelSO.E_GetPlayer += GetPlayer;
		}

		private void OnDisable()
		{
			agentManagerChannelSO.E_HighlightEnemies -= HighlightEnemies;
			agentManagerChannelSO.E_CancelHighlightEnemies -= CancelHighlightEnemies;
			agentManagerChannelSO.E_SetPlayer -= SetPlayer;
			agentManagerChannelSO.E_GetPlayer -= GetPlayer;
		}

		private void AddEnemy(Enemy enemyToAddd)
		{
			enemyContainer.Add(enemyToAddd);
		}

		private void RemoveEnemy(Enemy enemyToRemove)
		{
			enemyContainer.Remove(enemyToRemove);
		}

		private void SetPlayer(AgentBase _player)
		{
			player = _player;
		}

		private AgentBase GetPlayer()
		{
			return player;
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