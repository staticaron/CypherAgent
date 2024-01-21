using System.Collections;
using UnityEngine;

namespace Core
{
	public class Enemy : MonoBehaviour
	{
		[SerializeField] GameObject highlightObj;

		private bool isInHighlightMode = false;
		private Material highlightMaterial;

		private void Awake()
		{
			highlightMaterial = highlightObj.GetComponent<Renderer>().material;
		}

		public void EnableHighlights(float highlightTime)
		{
			if (isInHighlightMode) return;

			highlightMaterial.SetFloat("_Fade", 1);
			isInHighlightMode = true;

			StartCoroutine(FadeHighlights(highlightTime));

		}

		private IEnumerator FadeHighlights(float duration)
		{
			float newFade = highlightMaterial.GetFloat("_Fade");

			while (newFade > 0)
			{
				newFade = highlightMaterial.GetFloat("_Fade") - 1 / (duration / Time.deltaTime);
				highlightMaterial.SetFloat("_Fade", newFade);

				yield return null;
			}

			if (newFade <= 0) isInHighlightMode = false;
		}

		public void DisableHighlights()
		{
			highlightMaterial.SetFloat("_Fade", 0.0f);
		}
	}
}