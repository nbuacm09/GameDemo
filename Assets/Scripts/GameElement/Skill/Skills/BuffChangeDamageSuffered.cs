using UnityEngine;
using System.Collections;

public class BuffChangeDamageSuffered : BuffBase {
	protected override void OnBuffPastedOnCharacter () {
		target.damageSufferedPreprocess += DamageSufferedPreprocess;
	}

	protected override void OnSkillOver () {
		target.damageSufferedPreprocess -= DamageSufferedPreprocess;
	}

	void DamageSufferedPreprocess (ref double damage, SkillBase skill) {
		damage *= BuffConfig.intArgs [0];
	}
}
