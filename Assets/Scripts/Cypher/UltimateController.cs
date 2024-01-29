using DG.Tweening;
using Interfaces;
using SO;
using System.Collections;
using UnityEngine;

namespace Cypher
{
	public class UltimateController : MonoBehaviour, IAgentAbility
	{
		[Header("Ultimate Highlight Options")]
		[SerializeField] float highlightDuration;   // Highlight duration per beep.
		[SerializeField] int numberOfBeeps;         // Number of beeps
		[SerializeField] float timeBetweenBeeps;

		[Header("Ultimate Hat Options")]
		[SerializeField] Transform cypherHat;
		[SerializeField] Transform cypherCurvePoint;    // A point that makes up the curve that the hat follows.
		[SerializeField] Transform cypherHatStartPoint; // A point from where the hat starts moving

		[Header("Tweening Options")]
		[SerializeField] float hatSpeed;
		[SerializeField] float activationHeight;
		[SerializeField] float hatRotateSpeedDuringArc;
		[SerializeField] float hatRotateSpeedAfterActivation;

		[Header("SOs")]
		[SerializeField] AgentManagerChannelSO agentManagerChannelSO;

		private Transform mainCam;

		private Tweener hatRotator;

		private void Awake()
		{
			mainCam = Camera.main.transform;
		}

		public void StartAbility()
		{
			if (cypherHat.gameObject.activeInHierarchy) return;

			Ray ray = new Ray(mainCam.position, mainCam.forward);
			RaycastHit hitInfo;

			if (Physics.Raycast(ray, out hitInfo, 10))
			{
				Vector3[] hatArc = new Vector3[] { cypherCurvePoint.position, hitInfo.point, hitInfo.point + Vector3.up * activationHeight };

				cypherHat.gameObject.SetActive(true);
				cypherHat.transform.position = cypherHatStartPoint.position;

				cypherHat.DOPath(hatArc, hatSpeed, PathType.CatmullRom).SetEase(Ease.InOutQuad)
					.OnComplete(() =>
				{
					StopAllCoroutines();

					hatRotator.Kill();
					hatRotator = cypherHat.DORotate(new Vector3(0, 360, 0), hatRotateSpeedAfterActivation, RotateMode.LocalAxisAdd);
					hatRotator.SetEase(Ease.Linear);
					hatRotator.SetLoops(-1, LoopType.Incremental);

					StartCoroutine(UltimateBeeps());
				});

				hatRotator = cypherHat.DORotate(new Vector3(0, 360, 0), hatRotateSpeedDuringArc, RotateMode.LocalAxisAdd);
				hatRotator.SetEase(Ease.Linear);
				hatRotator.SetLoops(-1, LoopType.Incremental);
			}
		}

		private IEnumerator UltimateBeeps()
		{
			for (int x = 0; x < numberOfBeeps; x++)
			{
				agentManagerChannelSO.RaiseHighlightEnemies(highlightDuration);
				yield return new WaitForSeconds(timeBetweenBeeps);
			}

			hatRotator.Kill();

			cypherHat.gameObject.SetActive(false);
		}

		public void EndAbility()
		{
			StopAllCoroutines();
			agentManagerChannelSO.RaiseCancelHighlightEnemies();
		}
	}
}