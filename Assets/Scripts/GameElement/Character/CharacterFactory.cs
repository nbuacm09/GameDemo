using UnityEngine;
using System.Collections;

public class CharacterFactory : FactoryBase<CharacterFactory, CharacterBase> {
	protected override ConfigBaseObject GetConfig (string kindId) {
		return CharacterConfigManager.GetInstance ().GetCharacterConfig (kindId);
	}

	protected override CharacterBase CreateObject (string kindId) {
		var config = GetConfig (kindId) as CharacterConfigBase;
		if (config == null) {
			return null;
		} else {
			var character = DataFunc.CreateObject (config.characterType) as CharacterBase;
			if (character != null) {
				if (config.ai != "") {
					character.SetAi (DataFunc.CreateObject (config.ai) as AiBase);
				} else {
					character.SetAi (new AiBase ());
				}
			}
			return character;
		}
	}
}
