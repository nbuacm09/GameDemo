using UnityEngine;
using System.Collections;

public class AiBossHealther : AiBase {
	protected override bool SelectSkillAndTarget (ref string selectedSkillKindId, ref CharacterBase selectedTarget) {
		selectedTarget = battle.GetBattleGroup (character).Boss;

		SkillBase temp;
		foreach (var skillKindId in character.SkillList) {
			if (character.TryCreateSkill (skillKindId, ref selectedTarget, out temp) == SKILL_CAST_RESULT.SUCCESS) {
				selectedSkillKindId = skillKindId;
				return true;
			}
		}
		return false;
	}
}
