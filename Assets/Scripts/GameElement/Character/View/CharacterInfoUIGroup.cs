using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CharacterInfoUIGroup : MonoBehaviour {
	[SerializeField] List<CharacterInfoUIBase> views = new List<CharacterInfoUIBase> ();

	public void Init (CharacterBase character) {
		for (int i = 0; i < views.Count; i++) {
			views [i].SetCharacter (character);
		}
	}
}
