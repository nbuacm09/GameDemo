using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterFlyNumber : MonoBehaviour {
	[SerializeField] Text flyText;

	const float duration0 = 0.5f;
	const float duration1 = 1f;
	const float initAlpha = 0.5f;
	const float initScale = 0.5f;
	float passedTime = 0;
	Vector3 moveDis;
	Vector3 originalPos;
	// Use this for initialization
	void Start () {
		flyText.gameObject.SetAlpha (initAlpha);
		flyText.gameObject.SetScale (initScale);
		originalPos = transform.localPosition;
		moveDis = new Vector3((Random.value - 0.5f) * 100, 100 + (Random.value - 0.5f) * 20, 0);
	}
	
	// Update is called once per frame
	void Update () {
		passedTime += Time.deltaTime;
		if (passedTime <= duration0) {
			float process = passedTime / duration0;
			flyText.gameObject.SetAlpha ((1 - initAlpha) * process + initAlpha);
			flyText.gameObject.SetScale ((1 - initScale) * process + initScale);
			flyText.transform.localPosition = originalPos + moveDis * process;
		} else if (passedTime <= duration0 + duration1) {
			float process = (passedTime - duration0) / duration1;
			flyText.gameObject.SetAlpha (1 - process);
		} else {
			Destroy (gameObject);
		}
	}

	public void SetValue (string str, Color color) {
		flyText.text = str;
		flyText.color = color;


	}
}
