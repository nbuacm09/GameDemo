using UnityEngine;
using System.Collections;

public class BuffBombAfterHealth : BuffBase {
	protected override void Effective () {
		target.Damage (effectValue.Value * StackedCount, this);
	}

	protected override void OnSkillOver () {
		var buff = SkillFactory.GetInstance().Create("skill_0");
		buff.effectValue.AddChangeMethod (new ChangeMethod(ChangeType.MULTI, StackedCount));
		buff.CastTo (target, caster);
	}
}
