using UnityEngine;
using System.Collections;

public class PlayerManager : Singleton<PlayerManager> {
	CharacterBase player;
	public CharacterBase Player {
		get {
			return player;
		}
	}
	public CharacterBase Init (string characterKindId) {
		player = CharacterFactory.GetInstance ().Create (characterKindId);
		return player;
	}
}
