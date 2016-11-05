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
		skillCfg.intArgs.Add(20);
		skillCfg.manaCost = 40;
		skillCfg.cdTime = 1000;
		skillCfg.canTargetEnemy = true;
		skillCfg.canTargetFriend = false;
		skillConfigs.Add (skillCfg.kindId, skillCfg);

		skillCfg = new SkillConfigBase ();
		skillCfg.kindId = "skill_SkillDamage_1";
		skillCfg.skillType = "SkillDamage";
		skillCfg.name = "FireRain";
		skillCfg.intArgs.Add(20);
		skillCfg.manaCost = 80;
		skillCfg.cdTime = 5000;
		skillCfg.singTime = 2000;
		skillCfg.isAoe = true;
		skillCfg.canTargetEnemy = true;
		skillCfg.canTargetFriend = false;
		skillConfigs.Add (skillCfg.kindId, skillCfg);

		skillCfg = new SkillConfigBase ();
		skillCfg.kindId = "skill_SkillHealth_0";
		skillCfg.skillType = "SkillHealth";
		skillCfg.name = "cure";
		skillCfg.intArgs.Add(2);
		skillCfg.manaCost = 40;
		skillCfg.cdTime = 2000;
		skillCfg.canTargetEnemy = false;
		skillCfg.canTargetFriend = true;
		skillConfigs.Add (skillCfg.kindId, skillCfg);

		skillCfg = new SkillConfigBase ();
		skillCfg.kindId = "skill_SkillSummon_0";
		skillCfg.skillType = "SkillSummon";
		skillCfg.name = "summon skeleton";
		skillCfg.stringArgs.Add("character_Monster_1");
		skillCfg.manaCost = 30;
		skillCfg.cdTime = 0;
		skillCfg.singTime = 3000;
		skillCfg.canTargetEnemy = false;
		skillCfg.canTargetFriend = true;
		skillConfigs.Add (skillCfg.kindId, skillCfg);

		skillCfg = new SkillConfigBase ();
		skillCfg.kindId = "skill_SkillSummon_1";
		skillCfg.skillType = "SkillSummon";
		skillCfg.name = "summon healther";
		skillCfg.stringArgs.Add("character_Monster_2");
		skillCfg.manaCost = 30;
		skillCfg.cdTime = 1000;
		skillCfg.singTime = 0;
		skillCfg.canTargetEnemy = false;
		skillCfg.canTargetFriend = true;
		skillConfigs.Add (skillCfg.kindId, skillCfg);

		buffCfg = new BuffConfigBase();
		buffCfg.kindId = "buff_BuffHealth_0";
		buffCfg.skillType = "BuffHealth";
		buffCfg.name = "spring";
		buffCfg.duration = 5000;
		buffCfg.effectInterval = 500;
		buffCfg.intArgs.Add(1);
		buffCfg.maxStackedCount = 5;
		buffCfg.manaCost = 30;
		buffCfg.cdTime = 2000;
		buffCfg.singTime = 1000;
		buffCfg.canTargetEnemy = true;
		buffCfg.canTargetFriend = false;
		skillConfigs.Add (buffCfg.kindId, buffCfg);

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
		buffCfg.kindId = "buff_BuffManaAdd_0";
		buffCfg.skillType = "BuffManaAdd";
		buffCfg.name = "energy resume";
		buffCfg.isEndless = true;
		buffCfg.effectInterval = 100;
		buffCfg.intArgs.Add(1);
		buffCfg.maxStackedCount = 1;
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
