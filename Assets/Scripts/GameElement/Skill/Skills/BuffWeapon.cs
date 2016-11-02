using UnityEngine;
using System.Collections;

public class BuffWeapon : BuffBase {
	protected override void Effective () {
		if (target.SelectedTarget == null) {
			return;
		}
		Damage (BuffConfig.intArgs[0], target.SelectedTarget);
	}
}
