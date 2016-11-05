using UnityEngine;
using System.Collections;

public class BuffManaAdd: BuffBase {
	protected override void Effective () {
		target.AddProperty (PROPERTY.MP, BuffConfig.intArgs [0] * StackedCount);
	}
}
