using UnityEngine;
using System.Collections;

public class DurationBuffDot : DurationBuffBase {
	protected override void Effective () {
		character.Damage (effectValue.Value * StackedCount, this);
	}
}
