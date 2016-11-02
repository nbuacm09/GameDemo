using UnityEngine;
using System.Collections;

public class AiBase : BaseObject {
	CharacterBase character;
	public CharacterBase Character {
		get {
			return character;
		}
	}

	Battle battle;

	protected override void UnregisterAllDelegates () {
		if (character != null) {
			character.onDead -= OnDead;
		}
		GameTimeManager.UnregistBaseObject (this);
	}

	public void Control (CharacterBase character) {
		if (this.character != null) {
			UnregisterAllDelegates ();
		}
		this.battle = GameManager.GetInstance().CurrentBattle;
		this.character = character;
		character.onDead += OnDead;
		GameTimeManager.RegistBaseObject (this);
	}

	void OnDead (SkillBase skill) {
		Destroy ();
	}

	public void Remove () {
		Destroy ();
	}

	protected override void Update (long deltaTime) {
		if (character.SelectedTarget == null) {
			var randomTarget = GetRandomTarget ();
			character.SelectTarget (randomTarget);
		}
		string skillKindId = "";
		CharacterBase target = null;
		if (SelectSkillAndTarget (ref skillKindId, ref target)) {
			character.SelectTarget (target);
			character.CastSkill (skillKindId);
		}
	}

	protected virtual bool SelectSkillAndTarget (ref string selectedSkillKindId, ref CharacterBase selectedTarget) {
		selectedTarget = GetRandomTarget ();

		SkillBase temp;
		foreach (var skillKindId in character.SkillList) {
			if (character.CreateSkill (skillKindId, ref selectedTarget, out temp) == SKILL_CAST_RESULT.SUCCESS) {
				selectedSkillKindId = skillKindId;
				return true;
			}
		}
		return false;
	}

	CharacterBase GetRandomTarget () {
		int teamId = battle.GetCharacterTeam (character);
		int count = 0;
		foreach (var battleGroup in battle.GetBattleGroups()) {
			if (battleGroup.teamId == teamId) {
				continue;
			}
			foreach (var member in battleGroup.GetMembers()) {
				if (member.IsDead) {
					continue;
				}
				count++;
				if (Random.value <= 1.0f / count) {
					return member;
				}
			}
		}

		return null;
	}
}
