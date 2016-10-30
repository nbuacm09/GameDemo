using UnityEngine;
using System.Collections;

public class BuffHealth : BuffBase {
	protected override void Effective () {
		Health (BuffConfig.intArgs[0] * StackedCount);
	}
}
