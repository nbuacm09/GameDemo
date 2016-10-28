using UnityEngine;
using System.Collections;

public class CharacterInfoUIBase : MonoBehaviour {
	protected CharacterBase character;
	public CharacterBase Character {
		get {
			return character;
		}
	}

	public virtual void Init (CharacterBase character) {
		this.character = character;
	}
}
