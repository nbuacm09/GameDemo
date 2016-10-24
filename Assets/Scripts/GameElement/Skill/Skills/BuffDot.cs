using UnityEngine;
using System.Collections;

public class BuffDot : BuffBase {
	protected override void Effective () {
		target.Damage (effectValue.Value * StackedCount, this);
	}
}
