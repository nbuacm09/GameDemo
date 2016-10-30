using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class BattlePanel : MonoBehaviour {
	[SerializeField] GameObject followerViewPrefab;
	[SerializeField] GameObject bossViewPrefab;
	[SerializeField] GameObject[] bossContainer;
	[SerializeField] GameObject[] followersContainer;

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
		var battleGroups = battle.GetBattleGroups ();
		for (int i = 0; i < battleGroups.Count; i++) {
			int currentI = i;
			AddCharacter (bossViewPrefab, bossContainer [i], battleGroups [i].Boss);
			foreach (var follower in battleGroups[i].GetFollowers()) {
				AddCharacter (followerViewPrefab, followersContainer [i], follower);
			}

			battleGroups [i].onFollowerAdded += (CharacterBase follower) => {
				OnFollowerAdded (currentI, follower);
			};
		}
	}

	void OnFollowerAdded(int groupId, CharacterBase follower) {
		AddCharacter (followerViewPrefab, followersContainer [groupId], follower);
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
