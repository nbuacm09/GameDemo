using UnityEngine;
using System.Collections;

public interface IEffectable {
	DAMAGE_SOURCE_TYPE GetDamageSourceType();
	CharacterBase GetCaster();
}
