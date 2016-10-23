using UnityEngine;
using System.Collections;

public class SkillPunch : SkillBase {
	protected override void Effective () {
		character.Damage (effectValue.Value, this);
	}
}
