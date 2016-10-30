using UnityEngine;
using System.Collections;

public class ControlledCharacterUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Battle battle = GameManager.GetInstance ().CurrentBattle;
		Debug.Assert (battle != null);
		SetControlledCharacter (battle.ControlledCharacter);
		battle.onControlledCharacterChanged += OnControlledCharacterChanged;
	}

	void Destroy () {
		
	}

	void OnControlledCharacterChanged (CharacterBase character) {
		SetControlledCharacter (character);
	}

	void SetControlledCharacter (CharacterBase character) {
		var characterUIs = gameObject.GetComponents<CharacterInfoUIBase> ();
		foreach (CharacterInfoUIBase ui in characterUIs) {
			ui.SetCharacter (character);
		}
	}
}
