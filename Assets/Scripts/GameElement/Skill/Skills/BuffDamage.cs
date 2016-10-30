using UnityEngine;
using System.Collections;

public class BuffDamage : BuffBase {
	protected override void Effective () {
		Damage (BuffConfig.intArgs[0] * StackedCount);
	}
}
