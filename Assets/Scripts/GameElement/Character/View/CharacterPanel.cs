using UnityEngine;
using System.Collections;

public class CharacterPanel : CharacterInfoUIBase {
	[SerializeField] GameObject characterPrefab;

	GameObject meObject;
	GameObject targetObject;

	CharacterInfoView meView;
	CharacterInfoView targetView;

	void OnDestroy () {
		character.onTargetSwitched -= OnTargetSwitched;
	}

	public override void Init (CharacterBase character) {
		base.Init (character);

		meObject = Instantiate (characterPrefab);
		meView = meObject.GetComponent<CharacterInfoView> ();
		meView.Init (character);
		meObject.SetParent (gameObject);
		character.onTargetSwitched += OnTargetSwitched;

		SetTarget (character.Target);
	}

	void OnTargetSwitched (CharacterBase oriTarget, CharacterBase newTarget) {
		SetTarget (newTarget);
	}

	void SetTarget (CharacterBase newTarget) {
		if (newTarget == null) {
			if (targetObject != null) {
				targetObject.SetActive (false);
			}
		} else {
			if (targetObject == null) {
				targetObject = Instantiate (characterPrefab);
				targetView = targetObject.GetComponent<CharacterInfoView> ();
				targetObject.SetParent (gameObject);
			}
			targetView.Init (newTarget);
			targetObject.SetActive (true);
		}
	}
}
