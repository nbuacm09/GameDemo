using UnityEngine;
using System.Collections;

// ============== WARNING ==============
// No override, no use: Start, Destroy.
// =====================================
public abstract class CharacterInfoUIBase : MonoBehaviour {
	protected CharacterBase character;
	public CharacterBase Character {
		get {
			return character;
		}
	}

	bool isInit;

	public void SetCharacter (CharacterBase character) {
		if (this.character != null) {
			ClearOriginalCharacterInfo ();
		}
		this.character = character;
		if (isInit == false) {
			isInit = true;
			Init ();
		}
		if (this.character != null) {
			SetNewCharacterInfo ();
		}
	}

	void Start () {
		if (isInit == false) {
			isInit = true;
			Init ();
		}
	}

	protected virtual void Init () {
		
	}

	protected virtual void OnDestroy () {
		ClearOriginalCharacterInfo ();
	}
	protected abstract void ClearOriginalCharacterInfo ();
	protected abstract void SetNewCharacterInfo ();
}
