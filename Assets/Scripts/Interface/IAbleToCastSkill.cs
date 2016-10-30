using UnityEngine;
using System.Collections;

public interface IAbleToCastSkill {
	SKILL_CAST_RESULT CastSkill (string skillKindId, CharacterBase target = null) ;
	long GetId();
}
