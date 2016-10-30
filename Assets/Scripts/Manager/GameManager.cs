using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : Singleton<GameManager> {
	Battle currentBattle;
	public Battle CurrentBattle {
		get {
			return currentBattle;
		}
	}
	public void LoadGame () {
		// TODO
	}

	public void NewGame () {
		PlayerManager.GetInstance ().Init ("character_Warrior_0");
		currentBattle = new Battle ();
		currentBattle.RandomInit ();
		currentBattle.onGameEnd += OnGameEnd;
	}

	void OnGameEnd (bool win) {
		currentBattle.onGameEnd -= OnGameEnd;
		currentBattle = null;
		SceneManager.LoadScene ("Entry");
	}
}
