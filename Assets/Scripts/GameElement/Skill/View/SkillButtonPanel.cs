using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SkillButtonPanel : CharacterInfoUIBase {
	[SerializeField] GameObject skillButtonPrefab;

	public override void Init (CharacterBase character) {
		base.Init (character);
		// init skill
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
		var skillConfig = SkillConfigManager.GetInstance ().GetSkillConfig (skillKindId);
		CharacterBase target;
		if (skillConfig.isBenefit) {
			target = character;
		} else {
			target = character.Target;
		}

		if (target == null) {
			CastSkillWarning (SKILL_CAST_RESULT.NO_TARGET);
			return;
		}


		var res = character.CastSkill (skillKindId, target);
		//		var res = God.GetInstance().CastSkill (skillKindId, view.Character);
		if (res != SKILL_CAST_RESULT.SUCCESS) {
			CastSkillWarning (res);
		}
	}

	void CastSkillWarning (SKILL_CAST_RESULT res) {
		// TODO
	}
}
