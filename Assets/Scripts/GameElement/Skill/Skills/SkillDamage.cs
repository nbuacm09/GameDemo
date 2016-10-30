using UnityEngine;
using System.Collections;

public class SkillDamage : SkillBase {
	protected override void Effective () {
		Damage (SkillConfig.intArgs[0]);
	}
}
