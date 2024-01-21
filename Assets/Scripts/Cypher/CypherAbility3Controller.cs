using Interfaces;
using UnityEngine;

namespace Cypher
{
	public class CypherAbility3Controller : MonoBehaviour, IAgentAbility
	{
		[SerializeField] Entities.CageNade cageObject;

		[Header("SOs")]
		[SerializeField] SO.CypherMainChannelSO cypherMainChannelSO;

		public void StartAbility()
		{
			cypherMainChannelSO.RaiseSetState(Enums.AgentState.ABILITY3);

			cageObject.gameObject.SetActive(true);
			cageObject.Launch();

			cypherMainChannelSO.RaiseSetState(Enums.AgentState.NONE);
		}

		public void EndAbility() { }
	}
}
