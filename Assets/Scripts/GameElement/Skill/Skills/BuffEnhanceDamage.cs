using UnityEngine;
using System.Collections;

public class BuffEnhanceDamage : BuffBase {
	protected override void OnBuffPasteOnCharacter () {
		target.damageProcess += DamageProcess;
	}

	protected override void OnSkillOver () {
		target.damageProcess -= DamageProcess;
	}

	void DamageProcess (ref double damage) {
		damage *= effectValue.Value;
	}
}
