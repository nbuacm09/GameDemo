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
		skillCfg.kindId = "skill_SkillDamage_0";
		skillCfg.skillType = "SkillDamage";
		skillCfg.name = "punch";
		skillCfg.intArgs.Add(50);
		skillCfg.manaCost = 0;
		skillCfg.cdTime = 1000;
		skillCfg.canTargetEnemy = true;
		skillCfg.canTargetFriend = false;
		skillConfigs.Add (skillCfg.kindId, skillCfg);

		skillCfg = new SkillConfigBase ();
		skillCfg.kindId = "skill_SkillSummon_0";
		skillCfg.skillType = "SkillSummon";
		skillCfg.name = "summon";
		skillCfg.stringArgs.Add("character_Monster_1");
		skillCfg.manaCost = 100;
		skillCfg.cdTime = 0;
		skillCfg.singTime = 5000;
		skillCfg.canTargetEnemy = false;
		skillCfg.canTargetFriend = true;
		skillConfigs.Add (skillCfg.kindId, skillCfg);

		buffCfg = new BuffConfigBase();
		buffCfg.kindId = "buff_BuffDamage_0";
		buffCfg.skillType = "BuffDamage";
		buffCfg.name = "dot";
		buffCfg.duration = 5000;
		buffCfg.effectInterval = 500;
		buffCfg.intArgs.Add(1);
		buffCfg.maxStackedCount = 3;
		buffCfg.manaCost = 10;
		buffCfg.cdTime = 2000;
		buffCfg.singTime = 1000;
		buffCfg.canTargetEnemy = true;
		buffCfg.canTargetFriend = false;
		skillConfigs.Add (buffCfg.kindId, buffCfg);

		buffCfg = new BuffConfigBase();
		buffCfg.kindId = "buff_BuffEnhanceDamageSuffered_0";
		buffCfg.skillType = "BuffEnhanceDamageSuffered";
		buffCfg.name = "dmg*2";
		buffCfg.duration = 20000;
		buffCfg.effectInterval = -1;
		buffCfg.intArgs.Add(2);
		buffCfg.maxStackedCount = 1;
		buffCfg.manaCost = 10;
		buffCfg.cdTime = 3000;
		buffCfg.canTargetEnemy = true;
		buffCfg.canTargetFriend = false;
		skillConfigs.Add (buffCfg.kindId, buffCfg);

		buffCfg = new BuffConfigBase();
		buffCfg.kindId = "buff_BuffHealth_0";
		buffCfg.skillType = "BuffHealth";
		buffCfg.name = "health";
		buffCfg.duration = 20000;
		buffCfg.effectInterval = 2000;
		buffCfg.intArgs.Add(10);
		buffCfg.maxStackedCount = 1;
		buffCfg.manaCost = 10;
		buffCfg.cdTime = 0;
		buffCfg.canTargetEnemy = false;
		buffCfg.canTargetFriend = true;
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
