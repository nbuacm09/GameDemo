using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterPropertyBar : CharacterInfoUIBase {
	[SerializeField] GameObject processBar;
	[SerializeField] Text processText;
	[SerializeField] PROPERTY property;
	IProcessable processBarInterface;

	protected override void Init () {
		processBarInterface = processBar.GetComponent<IProcessable> ();
	}

	protected override void ClearOriginalCharacterInfo () {

	}

	protected override void SetNewCharacterInfo () {
		RefreshUI (true);
	}
	
	// Update is called once per frame
	void Update () {
		if (character == null) {
			return;
		}
		RefreshUI ();
	}

	void RefreshUI (bool force = false) {
		double process = 0;
		int val = character.GetProperty (property);
		int maxVal = character.GetPropertyMaxValue (property);
		if (maxVal == 0) {
			process = 0;
		} else {
			process = (double)val / maxVal;
		}

		if (processText != null) {
			processText.text = val + "/" + maxVal;
		}
		if (processBarInterface != null) {
			processBarInterface.SetProcess (process, force);
		}
	}
}
