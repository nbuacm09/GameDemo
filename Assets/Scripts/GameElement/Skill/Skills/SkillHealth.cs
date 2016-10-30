using UnityEngine;
using System.Collections;

public class SkillHealth : SkillBase {
	protected override void Effective () {
		Health (SkillConfig.intArgs[0]);
	}
}
