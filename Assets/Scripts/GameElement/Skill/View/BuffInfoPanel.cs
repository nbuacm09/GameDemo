using UnityEngine;
using System.Collections;

public class BuffInfoPanel : CharacterInfoUIBase {
	[SerializeField] GameObject buffViewPrefab;

	public override void Init (CharacterBase character) {
		base.Init (character);
		character.onNewBuffAppended += onNewBuffAppended;
	}

	void onNewBuffAppended (BuffBase buff) {
		var buffObject = Instantiate (buffViewPrefab);
		buffObject.SetParent (gameObject);
		var buffView = buffObject.GetComponent<BuffView> ();
		buffView.Init (buff);
	}
}
