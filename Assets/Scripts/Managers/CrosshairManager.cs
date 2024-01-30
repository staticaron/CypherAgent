using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
	public class CrosshairManager : MonoBehaviour
	{
		public static CrosshairManager Instance
		{
			get; private set;
		}

		[SerializeField] private Sprite defaultCrosshair;
		[SerializeField] private Sprite cypherCamCrossHair;

		[SerializeField] private Image crosshairDisplay;

		private void Awake()
		{
			if (Instance == null) Instance = this;
			else
			{
				Debug.LogWarning("Crosshair Manager Component was removed due to many instances of the singleton!");
				Destroy(this);
			}

			crosshairDisplay.sprite = defaultCrosshair;
		}

		public void SetCypherCrosshair()
		{
			crosshairDisplay.sprite = cypherCamCrossHair;
		}

		public void ResetCrosshair()
		{
			crosshairDisplay.sprite = defaultCrosshair;
		}
	}
}