using Enums;
using Interfaces;
using SO;
using UnityEngine;

namespace Cypher
{

	public class CypherAbility2Controller : MonoBehaviour, IAgentAbility
	{
		[SerializeField] LayerMask wallLayer;
		[SerializeField] float maxDeconSpawnDistance = 10;

		[Space]
		[SerializeField] bool active = false;

		[SerializeField] GameObject actualTripWireObj;
		[SerializeField] GameObject temporaryDeconObj;
		[SerializeField] Transform propsHolder;
		[SerializeField] Transform gunStartingPoint;
		[SerializeField] LineRenderer handLineRenderer;

		[SerializeField] float tripWireRange = 5;

		[Header("SOs")]
		[SerializeField] CypherMainChannelSO cypherMainChannelSO;
		[SerializeField] UIChannelSO uiChannelSO;

		private bool isInRange = false;
		private bool canSpawnTripWire = false;
		private Transform mainCamera;

		private void Awake()
		{
			mainCamera = Camera.main.transform;
		}

		private void LateUpdate()
		{
			if (!active) return;

			Ray ray = new Ray(mainCamera.position, mainCamera.forward);

			RaycastHit hitInfo;

			if (Physics.Raycast(ray, out hitInfo, maxDeconSpawnDistance, wallLayer))
			{
				isInRange = true;

				handLineRenderer.positionCount = 2;
				handLineRenderer.SetPosition(0, gunStartingPoint.position);
				handLineRenderer.SetPosition(1, hitInfo.point);

				// Position the decon
				if (!temporaryDeconObj.activeInHierarchy) temporaryDeconObj.SetActive(true);
				temporaryDeconObj.transform.position = hitInfo.point;
				temporaryDeconObj.transform.forward = hitInfo.normal;

				Ray tripWireRangeCheckRay = new Ray(temporaryDeconObj.transform.position, temporaryDeconObj.transform.forward);

				RaycastHit tripWireRangeCheckInfo;

				if (Physics.Raycast(tripWireRangeCheckRay, out tripWireRangeCheckInfo, tripWireRange, wallLayer))
				{
					canSpawnTripWire = true;
				}
				else
				{
					canSpawnTripWire = false;
				}
			}
			else
			{
				isInRange = false;

				temporaryDeconObj.SetActive(false);

				handLineRenderer.positionCount = 0;
			}
		}

		public void StartAbility()
		{
			cypherMainChannelSO.RaiseSetState(AgentState.ABILITY2);

			active = true;
			uiChannelSO.RaiseSetAbilityState(AbilityType.ABILITY_2, IconState.ACTIVATE);
		}

		public void EndAbility()
		{
			active = false;
			uiChannelSO.RaiseResetIconSize();

			handLineRenderer.positionCount = 0;

			if (temporaryDeconObj.activeInHierarchy) temporaryDeconObj.SetActive(false);

			cypherMainChannelSO.RaiseSetState(AgentState.NONE);
		}

		public void Primary()
		{
			if (!active) return;
			if (!isInRange) return;
			if (!canSpawnTripWire) return;

			SpawnTripWire();
			EndAbility();

			uiChannelSO.RaiseSetAbilityState(AbilityType.ABILITY_2, IconState.ACTIVE);
		}

		public void Secondary()
		{
			EndAbility();
		}

		private void SpawnTripWire()
		{
			GameObject instance = Instantiate<GameObject>(actualTripWireObj, temporaryDeconObj.transform.position, temporaryDeconObj.transform.rotation);
			instance.transform.parent = propsHolder;
		}
	}
}