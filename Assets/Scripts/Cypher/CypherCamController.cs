using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CypherCamController : MonoBehaviour
{
	[SerializeField] CinemachineVirtualCamera cypherCamera;
	[SerializeField] float lookAroundSpeed = 1;

	[SerializeField] bool active = true;

	[SerializeField] int elevatedPriority;
	[SerializeField] int defaultPriority;

	private void Update()
	{
		if (active) HandleCameraMovement();
	}

	private void HandleCameraMovement()
	{
		Vector2 mouseDelta = Mouse.current.delta.ReadValue();

		cypherCamera.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.Value += mouseDelta.x * lookAroundSpeed;
		cypherCamera.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.Value += mouseDelta.y * lookAroundSpeed;
	}

	public void StartDecon()
	{
		active = true;
		cypherCamera.Priority = elevatedPriority;
	}

	public void StopDecon()
	{
		active = false;
		cypherCamera.Priority = defaultPriority;
	}
}
