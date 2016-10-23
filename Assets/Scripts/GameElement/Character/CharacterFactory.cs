using UnityEngine;
using System.Collections;

public class CharacterFactory : FactoryBase<CharacterFactory, CharacterBase> {
	protected override ConfigBaseObject GetConfig (string kindId) {
		return CharacterConfigManager.GetInstance ().GetCharacterConfig (kindId);
	}

	protected override CharacterBase CreateObject (string kindId) {
		CharacterBase ret = null;
		switch (kindId) {
		case "character_0":
			ret = new Warrior ();
			break;
		case "character_1":
			ret = new TestMonster ();
			break;
		}
		return ret;
	}
}
