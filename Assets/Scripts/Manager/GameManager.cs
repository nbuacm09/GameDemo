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

		GameStart ();
	}

	void OnGameEnd (bool win) {
		currentBattle.onGameEnd -= OnGameEnd;
		currentBattle = null;
		GameEnd ();
		SceneManager.LoadScene ("Entry");
	}

	void GameStart () {
		GameTimeManager.GetInstance ().Start ();
	}

	void GameEnd () {
		GameTimeManager.GetInstance ().Stop ();
	}
}
