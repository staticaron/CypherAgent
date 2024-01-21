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

		[Header("SOs")]
		[SerializeField] AgentManagerChannelSO agentManagerChannelSO;

		private Transform mainCam;

		[SerializeField] Vector3 point;
		[SerializeField] float hatSpeed;

		private void Awake()
		{
			mainCam = Camera.main.transform;
		}

		public void StartAbility()
		{
			Ray ray = new Ray(mainCam.position, mainCam.forward);
			RaycastHit hitInfo;

			if (Physics.Raycast(ray, out hitInfo, 10))
			{
				Vector3[] hatArc = new Vector3[] { cypherCurvePoint.position, hitInfo.point };

				cypherHat.gameObject.SetActive(true);
				cypherHat.transform.position = cypherHatStartPoint.position;
				cypherHat.DOPath(hatArc, hatSpeed, PathType.CatmullRom).SetEase(Ease.InOutQuad).OnComplete(() =>
				{
					StopAllCoroutines();

					StartCoroutine(UltimateBeeps());
				});
			}
		}

		private IEnumerator UltimateBeeps()
		{
			for (int x = 0; x < numberOfBeeps; x++)
			{
				agentManagerChannelSO.RaiseHighlightEnemies(highlightDuration);
				yield return new WaitForSeconds(timeBetweenBeeps);
			}
		}

		public void EndAbility()
		{
			StopAllCoroutines();
			agentManagerChannelSO.RaiseCancelHighlightEnemies();
		}
	}
}