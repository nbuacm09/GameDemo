using UnityEngine;
using System.Collections;

public class SkillPunch : SkillBase {
	protected override void Effective () {
		target.Damage (effectValue.Value, this);
	}
}
