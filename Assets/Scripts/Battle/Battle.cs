using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Battle : BaseObject {
	List<CharacterBase> playerFollowers = new List<CharacterBase> ();
	List<CharacterBase> enemyFollowers = new List<CharacterBase> ();
	bool isEnd;

	CharacterBase enemyBoss;
	CharacterBase playerBoss;

	CharacterBase controlledCharacter;
	public CharacterBase ControlledCharacter {
		get {
			return controlledCharacter;
		}
	}

	public BaseDelegateV<bool> onGameEnd;

	void Clear () {
		playerFollowers.Clear ();
		enemyFollowers.Clear ();
		enemyBoss = null;
		playerBoss = null;
	}

	void UnregisterDelegateOnControlledCharacter () {
		if (controlledCharacter != null) {
			controlledCharacter.onDead -= OnControlledDead;
		}
	}

	protected override void UnregisterAllDelegates () {
		if (controlledCharacter != null) {
			controlledCharacter.onDead -= OnControlledDead;
		}
		if (playerBoss != null) {
			playerBoss.onDead -= OnPlayerDead;
		}
		if (enemyBoss != null) {
			enemyBoss.onDead -= OnBossDead;
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

	public void GetResult () {
		// TODO
	}

	public void RandomInit() {
		Clear ();
		AddCharacter (
			CharacterFactory.GetInstance ().Create ("character_1"),
			BATTLE_GROUP.ENEMY,
			BATTLE_JOB.BOSS
		);

		AddCharacter (
			PlayerManager.GetInstance().Player,
			BATTLE_GROUP.PLAYER,
			BATTLE_JOB.BOSS
		);

		OnCharacteroInitFinished ();
	}

	void OnCharacteroInitFinished () {
		controlledCharacter = playerBoss;
		controlledCharacter.onDead += OnControlledDead;

		enemyBoss.onDead += OnBossDead;
		playerBoss.onDead += OnPlayerDead;
	}

	void OnBossDead (SkillBase skill) {
		GameEnd (true);
	}

	void OnPlayerDead (SkillBase skill) {
		GameEnd (false);
	}

	void OnControlledDead (SkillBase skill) {
		controlledCharacter = null;
	}

	public void AddCharacter (CharacterBase character, BATTLE_GROUP battleGroup, BATTLE_JOB battleJob) {
		switch (battleJob) {
		case BATTLE_JOB.FOLLOWER:
			GetFollowers (battleGroup).Add (character);
			break;
		case BATTLE_JOB.BOSS:
			switch (battleGroup) {
			case BATTLE_GROUP.ENEMY:
				Debug.Assert (enemyBoss == null);
				enemyBoss = character;
				break;
			case BATTLE_GROUP.PLAYER:
				Debug.Assert (playerBoss == null);
				playerBoss = character;
				break;
			}
			break;
		}
	}

	public List<CharacterBase> GetFollowers (BATTLE_GROUP battleGroup) {
		switch (battleGroup) {
		case BATTLE_GROUP.ENEMY:
			return enemyFollowers;
		case BATTLE_GROUP.PLAYER:
			return playerFollowers;
		default:
			return new List<CharacterBase> ();
		}
	}

	public CharacterBase GetEnemyBoss () {
		return enemyBoss;
	}

	public CharacterBase GetPlayer () {
		return playerBoss;
	}

	public void SetControlledCharacter (CharacterBase character) {
		UnregisterDelegateOnControlledCharacter ();
		controlledCharacter = character;
	}
}
