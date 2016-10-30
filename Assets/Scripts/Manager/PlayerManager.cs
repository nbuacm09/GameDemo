using UnityEngine;
using System.Collections;

public class PlayerInfo {
	public string characterKindId;
}

public class PlayerManager : Singleton<PlayerManager> {
	PlayerInfo player;
	public PlayerInfo Player{
		get {
			return player;
		}
	}

	public CharacterBase CreateCharacter () {
		return CharacterFactory.GetInstance ().Create (player.characterKindId);
	}

	public void Init (string characterKindId) {
		player = new PlayerInfo ();
		player.characterKindId = characterKindId;
	}
}
