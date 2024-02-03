using Enums;
using SO;
using UnityEngine;
using UnityEngine.UI;

public class AbilityIconManager : MonoBehaviour
{
	public static AbilityIconManager Instnace { get; private set; }

	[SerializeField] Image ability1Image;
	[SerializeField] Image ability2Image;
	[SerializeField] Image ability3Image;
	[SerializeField] Image ultImage;

	[SerializeField] Color activeColor;
	[SerializeField] Color disabledColor;

	[SerializeField] float defaultSize;
	[SerializeField] float activatingSize;

	[Header("SOs")]
	[SerializeField] UIChannelSO uiChannelSO;

	private void Awake()
	{
		if (Instnace == null) Instnace = this;
		else Destroy(gameObject);
	}

	private void OnEnable()
	{
		uiChannelSO.E_SetAbilityState += SetState;
		uiChannelSO.E_ResetIconSize += ResetIconSize;
	}

	private void OnDisable()
	{
		uiChannelSO.E_SetAbilityState -= SetState;
		uiChannelSO.E_ResetIconSize -= ResetIconSize;
	}

	public void SetState(AbilityType type, IconState state)
	{
		if (state == IconState.ACTIVATE)
		{
			ResetIconSize();
			GetAbilityImageByAbilityType(type).rectTransform.sizeDelta = Vector2.one * activatingSize;
		}
		else if (state == IconState.ACTIVE)
		{
			GetAbilityImageByAbilityType(type).color = activeColor;
		}
		else if (state == IconState.DISABLED)
		{
			GetAbilityImageByAbilityType(type).color = disabledColor;
		}
		else if (state == IconState.INACTIVE)
		{
			GetAbilityImageByAbilityType(type).color = Color.white;
		}
	}

	public void ResetIconSize()
	{
		ability1Image.rectTransform.sizeDelta = Vector2.one * defaultSize;

		ability2Image.rectTransform.sizeDelta = Vector2.one * defaultSize;

		ability3Image.rectTransform.sizeDelta = Vector2.one * defaultSize;

		ultImage.rectTransform.sizeDelta = Vector2.one * defaultSize;
	}

	public Image GetAbilityImageByAbilityType(AbilityType type)
	{
		switch (type)
		{
			case AbilityType.ABILITY_1:
				return ability1Image;
			case AbilityType.ABILITY_2:
				return ability2Image;
			case AbilityType.ABILITY_3:
				return ability3Image;
			case AbilityType.ULT:
				return ultImage;
			default:
				return null;
		}
	}
}
