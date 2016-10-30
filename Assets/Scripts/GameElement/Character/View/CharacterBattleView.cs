using UnityEngine;
using System.Collections;

public class CharacterBattleView : CharacterInfoUIBase {
	protected override void ClearOriginalCharacterInfo () {
		character.onDead -= OnDead;
	}

	protected override void SetNewCharacterInfo () {
		character.onDead += OnDead;
	}

	void OnDead (SkillBase skill) {
		Destroy (gameObject);
	}
}
