using UnityEngine;
using System.Collections;

public class SkillFactory : FactoryBase<SkillFactory, SkillBase> {
	protected override ConfigBaseObject GetConfig (string kindId) {
		return SkillConfigManager.GetInstance ().GetSkillConfig (kindId);
	}

	protected override SkillBase CreateObject (string kindId) {
		var config = GetConfig (kindId) as SkillConfigBase;
		if (config == null) {
			return null;
		} else {
			return DataFunc.CreateObject (config.skillType) as SkillBase;
		}
	}
}
