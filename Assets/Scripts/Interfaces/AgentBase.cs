using UnityEngine;

namespace Interfaces
{
	public abstract class AgentBase : MonoBehaviour, IAgent
	{
		[SerializeField] string agentIdentifier;

		public virtual void SetName(string _name) { agentIdentifier = _name; }

		public virtual string GetName() { return agentIdentifier; }

		public Transform GetTransform()
		{
			return transform;
		}
	}
}