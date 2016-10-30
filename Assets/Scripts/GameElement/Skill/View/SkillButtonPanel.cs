using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SkillButtonPanel : CharacterInfoUIBase {
	[SerializeField] GameObject skillButtonPrefab;

	protected override void ClearOriginalCharacterInfo () {
		gameObject.ClearChildren ();
	}

	protected override void SetNewCharacterInfo () {
		foreach (string skillKindId in character.SkillList)
		{
			string curSkillKindId = skillKindId;
			var obj = Instantiate (skillButtonPrefab);
			obj.SetParent (gameObject);
			var skillButtonScript = obj.GetComponent<SkillButton> ();
			skillButtonScript.Init (character, skillKindId);
			var skillButton = obj.GetComponent<Button> ();
			skillButton.onClick.AddListener (() => {
				CastSkill(curSkillKindId);
			});
		}
	}

	void CastSkill (string skillKindId) {
		var res = character.CastSkill (skillKindId);
		if (res != SKILL_CAST_RESULT.SUCCESS) {
			CastSkillWarning (res);
		}
	}

	void CastSkillWarning (SKILL_CAST_RESULT res) {
		// TODO
	}
}
