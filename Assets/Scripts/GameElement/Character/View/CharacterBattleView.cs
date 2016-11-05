using UnityEngine;
using System.Collections;

public class CharacterBattleView : CharacterInfoUIBase {
	[SerializeField] GameObject characterNumberFlyPrefab;
	protected override void ClearOriginalCharacterInfo () {
		character.onDead -= OnDead;
		character.onDamaged -= OnDamaged;
		character.onHealthed -= OnHealthed;
	}

	protected override void SetNewCharacterInfo () {
		character.onDead += OnDead;
		character.onDamaged += OnDamaged;
		character.onHealthed += OnHealthed;
	}

	void OnDead (SkillBase skill) {
		Destroy (gameObject);
	}

	void OnDamaged (int val, SkillBase skill) {
		NumberFly ("-" + val, Color.red);
	}

	void OnHealthed (int val, SkillBase skill) {
		NumberFly ("+" + val, Color.green);
	}

	void NumberFly (string str, Color color) {
		GameObject obj = Instantiate (characterNumberFlyPrefab);
		CharacterFlyNumber script = obj.GetComponent<CharacterFlyNumber> ();
		obj.SetParent (gameObject);
		script.SetValue (str, color);
	}
}
