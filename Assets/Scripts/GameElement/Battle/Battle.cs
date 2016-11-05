using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Battle : BaseObject {
	public class BattleGroup {
		public int groupId;
		public int teamId;
		protected CharacterBase boss;
		public CharacterBase Boss {
			get {
				return boss;
			}
		}
		protected List<CharacterBase> followers = new List<CharacterBase> ();

		public BaseDelegateV<CharacterBase> onFollowerAdded;
		public BaseDelegateV<CharacterBase> onFollowerRemoved;

		public IEnumerable<CharacterBase> GetFollowers () {
			for (int i = 0; i < followers.Count; i++) {
				yield return followers [i];
			}
		}

		public IEnumerable<CharacterBase> GetMembers() {
			yield return boss;
			for (int i = 0; i < followers.Count; i++) {
				yield return followers [i];
			}
		}

		public void SetBoss (CharacterBase boss) {
			this.boss = boss;
		}

		public void AddFollower (CharacterBase character) {
			followers.Add (character);
			if (onFollowerAdded != null) {
				onFollowerAdded (character);
			}
		}

		public void RemoveFollower (CharacterBase character) {
			followers.Remove (character);
			if (onFollowerRemoved != null) {
				onFollowerRemoved (character);
			}
		}
	}
	bool isEnd;

	List<BattleGroup> battleGroups = new List<BattleGroup> ();
	Dictionary<CharacterBase, BattleGroup> character2Group = new Dictionary<CharacterBase, BattleGroup> ();
	BattleGroup PlayerGroup {
		get {
			return battleGroups.Count == 0 ? null : battleGroups [0];
		}
	}

	CharacterBase controlledCharacter;
	public CharacterBase ControlledCharacter {
		get {
			return controlledCharacter;
		}
	}

	public BaseDelegateV<bool> onGameEnd;
	public BaseDelegateV<CharacterBase> onControlledCharacterChanged;

	~Battle () {
		Debug.Log ("battle is deleted successfully");
	}

	void Clear () {
		battleGroups.Clear ();
	}

	void UnregisterControlledCharacterDelegate () {
		if (controlledCharacter != null) {
			controlledCharacter.onDead -= OnControlledDead;
		}
	}

	protected override void UnregisterAllDelegates () {
		// You needn't unregister character delegate here. 
		// Because if battle is destroyed, all characters of this battle shall be destroyed,
		// both battle & characters won't be visited by others, then they will be deleted automatically, even
	}

	public bool SameTeam (CharacterBase a, CharacterBase b) {
		return GetCharacterTeam (a) == GetCharacterTeam (b);
	}

	public int GetCharacterTeam (CharacterBase character) {
		if (character2Group.ContainsKey(character)) {
			return character2Group [character].teamId;
		} else {
			return -1;
		}
	}

	public List<BattleGroup> GetBattleGroups () {
		return battleGroups;
	}

	public BattleGroup GetBattleGroup (CharacterBase character) {
		if (character2Group.ContainsKey(character)) {
			return character2Group [character];
		} else {
			return null;
		}
	}

	protected void GameEnd (bool win) {
		if (isEnd) {
			return;
		}
		isEnd = true;
		if (onGameEnd != null) {
			onGameEnd (win);
		}
	}

	public void RandomInit() {
		Clear ();

		// group 0
		AddGroup (0, PlayerManager.GetInstance ().CreateCharacter());

		// group 1
		CharacterBase enemy;
		enemy = CharacterFactory.GetInstance ().Create ("character_Monster_0");
		AddGroup (1, enemy);
		OnCharacteroInitFinished ();
	}

	void OnCharacteroInitFinished () {
		SetControlledCharacter (PlayerGroup.Boss);
		controlledCharacter.onDead += OnControlledDead;
	}

	public virtual bool CheckGameEnd (ref bool win) {
		int playerTeam = PlayerGroup.teamId;
		bool playerTeamAllDead = true;
		bool playerEnemyTeamAllDead = true;
		for (int i = 0; i < battleGroups.Count; i++) {
			if (battleGroups[i].Boss.IsDead) {
				continue;
			} else {
				if (battleGroups[i].teamId == playerTeam) {
					playerTeamAllDead = false;
				} else {
					playerEnemyTeamAllDead = false;
				}
			}
		}

		win = playerTeamAllDead == false && playerEnemyTeamAllDead;
		bool gameEnd = playerTeamAllDead || playerEnemyTeamAllDead;
		return gameEnd;
	}

	void OnBossDead (SkillBase skill) {
		bool win = false;
		if (CheckGameEnd(ref win)) {
			GameEnd (win);
		}
	}

	void OnControlledDead (SkillBase skill) {
		controlledCharacter = null;
	}

	public void AddGroup (int groupId, CharacterBase boss, int teamId = -1) {
		if (teamId == -1) {
			teamId = groupId;
		}
		BattleGroup battleGroup = new BattleGroup ();
		battleGroup.groupId = groupId;
		battleGroup.teamId = teamId;
		battleGroup.SetBoss(boss);
		boss.onDead += OnBossDead;

		battleGroups.Add (battleGroup);

		character2Group [boss] = battleGroup;
	}

	public void AddFollower (int groupId, CharacterBase follower) {
		character2Group [follower] = battleGroups [groupId];
		battleGroups [groupId].AddFollower (follower);
		follower.onDestroy += () => {
			RemoveFollower(groupId, follower);
		};
	}

	public void AddFollower (CharacterBase character, CharacterBase follower) {
		AddFollower (GetBattleGroup (character).groupId, follower);
	}

	public void RemoveFollower (int groupId, CharacterBase character) {
		character2Group.Remove (character);
		battleGroups [groupId].RemoveFollower (character);
	}

	public IEnumerable<CharacterBase> GetFollowers (int groupId) {
		return battleGroups [groupId].GetFollowers();
	}

	public void SetControlledCharacter (CharacterBase character) {
		UnregisterControlledCharacterDelegate ();
		controlledCharacter = character;
		character.EnableAi (false);
		if (onControlledCharacterChanged != null) {
			onControlledCharacterChanged (character);
		}
	}
}
