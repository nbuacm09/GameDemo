using UnityEngine;
using System.Collections;

public class DurationBuffBombAfterHealth : DurationBuffBase {
	protected override void Effective () {
		character.Damage (effectValue.Value * StackedCount, this);
	}

	protected override void OnBuffOver () {
		var buff = BuffFactory.GetInstance().CreateBuff("buff_0");
		buff.effectValue.AddChangeMethod (new ChangeMethod(ChangeType.MULTI, StackedCount));
		buff.CastTo (character, caster);
	}
}
