using UnityEngine;
using System.Collections;

public class BuffBombAfterHealth : BuffBase {
	protected override void Effective () {
		character.Damage (effectValue.Value * StackedCount, this);
	}

	protected override void OnSkillOver () {
		var buff = SkillFactory.GetInstance().Create("skill_0");
		buff.effectValue.AddChangeMethod (new ChangeMethod(ChangeType.MULTI, StackedCount));
		buff.CastTo (character, caster);
	}
}
