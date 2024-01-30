using UnityEngine;
using UnityEngine.UI;

public class SnapCrossHair : MonoBehaviour
{
	[SerializeField] Image snapCrosshairImage;

	private void OnEnable()
	{
		SnapToEnemy.E_SnapUpdate += UpdateSnapCursor;
	}

	private void OnDisable()
	{
		SnapToEnemy.E_SnapUpdate -= UpdateSnapCursor;
	}

	private void UpdateSnapCursor(Vector3? _newPosition)
	{
		if (_newPosition == null)
		{
			snapCrosshairImage.enabled = false;
			return;
		}

		if (!snapCrosshairImage.enabled) snapCrosshairImage.enabled = true;
		snapCrosshairImage.rectTransform.position = _newPosition.Value;
	}
}
