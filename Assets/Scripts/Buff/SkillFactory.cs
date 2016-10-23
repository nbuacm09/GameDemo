using UnityEngine;
using System.Collections;

public class SkillFactory : FactoryBase<SkillFactory, SkillBase> {
	protected override ConfigBaseObject GetConfig (string kindId) {
		return SkillConfigManager.GetInstance ().GetSkillConfig (kindId);
	}

	protected override SkillBase CreateObject (string kindId) {
		SkillBase ret = null;
		switch (kindId) {
		case "skill_0":
			ret = new SkillPunch ();
			break;


		case "buff_0":
			ret = new BuffDot ();
			break;
		case "buff_1":
			ret = new BuffEnhanceDamage ();
			break;
		case "buff_2":
			ret = new BuffBombAfterHealth ();
			break;
		}
		return ret;
	}
}
