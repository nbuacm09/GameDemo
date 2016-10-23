using UnityEngine;
using System.Collections;

public interface IEffectable {
	EFFECT_PROPERTY_TYPE GetEffectProperty();
	CharacterBase GetCaster();
}
