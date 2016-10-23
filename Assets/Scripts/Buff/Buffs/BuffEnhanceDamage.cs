using UnityEngine;
using System.Collections;

public class BuffEnhanceDamage : BuffBase {
	protected override void OnBuffPasteOnCharacter () {
		character.damageProcess += DamageProcess;
	}

	protected override void OnSkillOver () {
		character.damageProcess -= DamageProcess;
	}

	void DamageProcess (ref double damage) {
		damage *= effectValue.Value;
	}
}
