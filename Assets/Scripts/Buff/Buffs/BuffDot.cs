using UnityEngine;
using System.Collections;

public class BuffDot : BuffBase {
	protected override void Effective () {
		character.Damage (effectValue.Value, this);
	}
}
