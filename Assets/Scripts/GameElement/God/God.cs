using UnityEngine;
using System.Collections;

public class God : BaseObject, IAbleToCastSkill {
	static God instance = null;
	static public God GetInstance() {
		if (instance == null) {
			instance = new God ();
		}
		return instance;
	}
	public SKILL_CAST_RESULT CastSkill(string skillKindId, CharacterBase target) {
		var skill = SkillFactory.GetInstance ().Create (skillKindId);

		skill.CastTo (target, this);

		return SKILL_CAST_RESULT.SUCCEED;
	}
}
