﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class CharacterConfigManager : Singleton<CharacterConfigManager> {
	Dictionary<string, CharacterConfigBase> characterConfigs = new Dictionary<string, CharacterConfigBase> ();

	public CharacterConfigManager () {
		Init ();
	}

	public void Init () {
		ReadConfig ();
	}

	void ReadConfig () {
		CharacterConfigBase characterCfg;

		characterCfg = new CharacterConfigBase ();
		characterCfg.kindId = "character_Warrior_0";
		characterCfg.characterType = "Warrior";
		characterCfg.name = "warrior";
		characterCfg.hp = 1000;
		characterCfg.mp = 100;
		characterCfg.skillKindIdList.Add ("skill_SkillDamage_0");
		characterConfigs.Add (characterCfg.kindId, characterCfg);

		characterCfg = new CharacterConfigBase ();
		characterCfg.kindId = "character_Monster_0";
		characterCfg.characterType = "Monster";
		characterCfg.name = "Big Mage";
		characterCfg.hp = 1000;
		characterCfg.mp = 1000;
		characterCfg.skillKindIdList.Add ("skill_SkillSummon_0");
		characterConfigs.Add (characterCfg.kindId, characterCfg);

		characterCfg = new CharacterConfigBase ();
		characterCfg.kindId = "character_Monster_1";
		characterCfg.characterType = "Monster";
		characterCfg.name = "Small Gost";
		characterCfg.hp = 100;
		characterCfg.mp = 200;
		characterCfg.skillKindIdList.Add ("skill_SkillSummon_0");
		characterConfigs.Add (characterCfg.kindId, characterCfg);
	}

	public CharacterConfigBase GetCharacterConfig (string kindId) {
		if (characterConfigs.ContainsKey(kindId)) {
			return characterConfigs [kindId];
		} else {
			return null;
		}
	}
}
