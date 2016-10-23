using UnityEngine;
using System.Collections;

public interface IEffectable {
	EFFECT_PROPERTY GetEffectProperty();
	CharacterBase GetCaster();
}
