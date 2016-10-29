using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class SkillConfigManager : Singleton<SkillConfigManager> {
	Dictionary<string, SkillConfigBase> skillConfigs = new Dictionary<string, SkillConfigBase> ();

	public SkillConfigManager () {
		Init ();
	}

	public void Init () {
		ReadConfig ();
	}

	void ReadConfig () {
		SkillConfigBase skillCfg;
		BuffConfigBase buffCfg;

		skillCfg = new SkillConfigBase ();
		skillCfg.kindId = "skill_0";
		skillCfg.name = "punch";
		skillCfg.effectValue = 10;
		skillCfg.manaCost = 10;
		skillCfg.cdTime = 1000;
		skillConfigs.Add (skillCfg.kindId, skillCfg);

		buffCfg = new BuffConfigBase();
		buffCfg.kindId = "buff_0";
		buffCfg.name = "dot";
		buffCfg.duration = 5000;
		buffCfg.effectInterval = 500;
		buffCfg.effectValue = 1;
		buffCfg.maxStackedCount = 3;
		buffCfg.manaCost = 10;
		buffCfg.cdTime = 2000;
		buffCfg.singTime = 1000;
		skillConfigs.Add (buffCfg.kindId, buffCfg);

		buffCfg = new BuffConfigBase();
		buffCfg.kindId = "buff_1";
		buffCfg.name = "dmg*2";
		buffCfg.duration = 20000;
		buffCfg.effectInterval = -1;
		buffCfg.effectValue = 2;
		buffCfg.maxStackedCount = 1;
		buffCfg.manaCost = 10;
		buffCfg.cdTime = 3000;
		skillConfigs.Add (buffCfg.kindId, buffCfg);

		buffCfg = new BuffConfigBase();
		buffCfg.kindId = "buff_2";
		buffCfg.name = "health bomb";
		buffCfg.duration = 5000;
		buffCfg.effectInterval = 500;
		buffCfg.effectValue = -1;
		buffCfg.maxStackedCount = 3;
		buffCfg.manaCost = 10;
		buffCfg.cdTime = 4000;
		skillConfigs.Add (buffCfg.kindId, buffCfg);

		buffCfg = new BuffConfigBase();
		buffCfg.kindId = "buff_3";
		buffCfg.name = "health";
		buffCfg.duration = 20000;
		buffCfg.effectInterval = 3000;
		buffCfg.effectValue = -10;
		buffCfg.maxStackedCount = 1;
		buffCfg.manaCost = 10;
		buffCfg.cdTime = 0;
		buffCfg.isBenefit = true;
		skillConfigs.Add (buffCfg.kindId, buffCfg);

	}

	public SkillConfigBase GetSkillConfig (string kindId) {
		if (skillConfigs.ContainsKey(kindId)) {
			return skillConfigs [kindId];
		} else {
			return null;
		}
	}
}
