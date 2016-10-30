using UnityEngine;
using System.Collections;

public class SkillSummon : SkillBase {
	int groupId;
	protected override void Effective () {
		var summonedCharacter = CharacterFactory.GetInstance ().Create (SkillConfig.stringArgs [0]);
		summonedCharacter.SetAi (new AiBase ());
		Summon (summonedCharacter);
	}

	void Summon (CharacterBase follower) {
		var battle = GameManager.GetInstance ().CurrentBattle;
		Debug.Assert (battle != null);
		battle.AddFollower(target, follower);
	}

	public override SKILL_CAST_RESULT CheckBeforeCast(CharacterBase caster, CharacterBase target) {
		var battle = GameManager.GetInstance ().CurrentBattle;
		Debug.Assert (battle != null);
		var battleGroup = battle.GetBattleGroup (target);
		Debug.Assert (battleGroup != null);

		return SKILL_CAST_RESULT.SUCCESS;
	}
}
