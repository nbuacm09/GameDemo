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
		characterCfg.kindId = "character_0";
		characterCfg.name = "warrior";
		characterCfg.hp = 100;
		characterCfg.mp = 100;
		characterCfg.skillKindIdList.Add ("skill_0");
		characterCfg.skillKindIdList.Add ("buff_0");
		characterConfigs.Add (characterCfg.kindId, characterCfg);

		characterCfg = new CharacterConfigBase ();
		characterCfg.kindId = "character_1";
		characterCfg.name = "monster";
		characterCfg.hp = 100;
		characterCfg.mp = 10;
		characterCfg.skillKindIdList.Add ("buff_0");
		characterCfg.skillKindIdList.Add ("buff_1");
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