using UnityEngine;
using System.Collections;

public class BuffInfoPanel : CharacterInfoUIBase {
	[SerializeField] GameObject buffViewPrefab;

	protected override void ClearOriginalCharacterInfo () {
		character.onNewBuffAppended -= onNewBuffAppended;
		gameObject.ClearChildren ();
	}

	protected override void SetNewCharacterInfo () {
		character.onNewBuffAppended += onNewBuffAppended;
		foreach (var buff in character.GetBuffList()) {
			AddBuff (buff);
		}
	}

	void onNewBuffAppended (BuffBase buff) {
		AddBuff (buff);
	}

	void AddBuff (BuffBase buff) {
		var buffObject = Instantiate (buffViewPrefab);
		buffObject.SetParent (gameObject);
		var buffView = buffObject.GetComponent<BuffView> ();
		buffView.Init (buff);
	}
}
