using UnityEngine;
using System.Collections;

public class CharacterSingView : CharacterInfoUIBase {
	[SerializeField] GameObject singBar;
	[SerializeField] ProcessBar singProcessBar;

	protected override void ClearOriginalCharacterInfo () {

	}

	protected override void SetNewCharacterInfo () {
		Update ();
	}

	void Update () {
		if (character == null) {
			singBar.SetActive (false);
			return;
		}
		if (character.IsSingingSkill) {
			singBar.SetActive (true);
			singProcessBar.SetProcess (1 - (double)character.SkillSingTimeLeft / character.SkillSingTime);
		} else {
			singBar.SetActive (false);
		}
	}
}
