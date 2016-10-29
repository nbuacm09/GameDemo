using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class BattlePanel : MonoBehaviour {
	[SerializeField] GameObject followerViewPrefab;
	[SerializeField] GameObject bossViewPrefab;
	[SerializeField] GameObject playerViewContainer;
	[SerializeField] GameObject enemyBossViewContainer;
	[SerializeField] GameObject playerFollowersViewContainer;
	[SerializeField] GameObject enemyFollowersViewContainer;

	Battle battle;
	public Battle BattleInfo {
		get {
			return battle;
		}
	}
	public void Init (Battle battle) {
		this.battle = battle;
		InitUI ();
	}

	void InitUI () {
		// player
		AddCharacter (bossViewPrefab, playerViewContainer, battle.GetPlayer ());

		foreach (var follower in battle.GetFollowers(BATTLE_GROUP.PLAYER)) {
			AddCharacter (followerViewPrefab, playerFollowersViewContainer, follower);
		}

		// enemy
		AddCharacter (bossViewPrefab, enemyBossViewContainer, battle.GetEnemyBoss ());

		foreach (var follower in battle.GetFollowers(BATTLE_GROUP.PLAYER)) {
			

			AddCharacter (followerViewPrefab, enemyFollowersViewContainer, follower);
		}
	}

	void AddCharacter (GameObject prefab, GameObject container, CharacterBase character) {
		GameObject obj = Instantiate(prefab);
		obj.SetParent (container);
		obj.GetComponent<CharacterInfoUIGroup> ().Init (character);
		obj.GetComponent<Button> ().onClick.AddListener (() => {
			if (battle.ControlledCharacter != null) {
				battle.ControlledCharacter.SelectTarget(character);
			}
		});
	}
}
