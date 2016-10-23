using UnityEngine;
using System.Collections;

public class BuffPunch : BuffBase {
	protected override void Effective () {
		character.Damage (effectValue.Value, this);
	}
}
