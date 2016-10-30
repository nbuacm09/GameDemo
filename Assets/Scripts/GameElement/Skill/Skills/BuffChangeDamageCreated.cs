using UnityEngine;
using System.Collections;

public class BuffChangeDamageCreated : BuffBase {
	protected override void OnBuffPastedOnCharacter () {
		target.castedSkillProcess += CastedSkillProcess;
	}

	protected override void OnSkillOver () {
		target.castedSkillProcess -= CastedSkillProcess;
	}

	void CastedSkillProcess (ref SkillBase skill) {
		skill.casterInfluence.damageMulti *= SkillConfig.intArgs[0];
	}
}
