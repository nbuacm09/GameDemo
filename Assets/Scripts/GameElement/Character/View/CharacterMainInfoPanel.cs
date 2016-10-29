using UnityEngine;
using System.Collections;

public class CharacterMainInfoPanel : CharacterInfoUIBase {
	[SerializeField] GameObject characterTopLeftPrefab;
	[SerializeField] GameObject characterTopRightPrefab;

	GameObject meObject;
	GameObject targetObject;

	CharacterInfoUIGroup meView;
	CharacterInfoUIGroup targetView;

	protected override void Init () {
		meObject = Instantiate (characterTopLeftPrefab);
		meView = meObject.GetComponent<CharacterInfoUIGroup> ();
		meObject.SetParent (gameObject);
	}

	protected override void ClearOriginalCharacterInfo () {
		character.onTargetSwitched -= OnTargetSwitched;
	}

	protected override void SetNewCharacterInfo () {
		meView.Init (character);
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
				targetObject = Instantiate (characterTopRightPrefab);
				targetView = targetObject.GetComponent<CharacterInfoUIGroup> ();
				targetObject.SetParent (gameObject);
			}
			targetView.Init (newTarget);
			targetObject.SetActive (true);
		}
	}
}
