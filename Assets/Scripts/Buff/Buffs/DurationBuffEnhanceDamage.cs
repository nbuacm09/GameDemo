using UnityEngine;
using System.Collections;

public class DurationBuffEnhanceDamage : DurationBuffBase {
	protected override void OnBuffPasteOnCharacter () {
		character.damageProcess += DamageProcess;
	}

	protected override void OnBuffOver () {
		character.damageProcess -= DamageProcess;
	}

	void DamageProcess (ref double damage) {
		damage *= effectValue.Value;
	}
}
