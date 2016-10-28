using UnityEngine;
using System.Collections;

public class SkillSingPanel : CharacterInfoUIBase {
	[SerializeField] GameObject singBar;
	[SerializeField] ProcessBar singProcessBar;

	void Update () {
		if (character.IsSingingSkill) {
			singBar.SetActive (true);
			singProcessBar.SetProcess (1 - (float)character.SkillSingTimeLeft / character.SkillSingTime);
		} else {
			singBar.SetActive (false);
		}
	}

	public override void Init (CharacterBase character) {
		base.Init (character);
		
	}
}
