using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class Test : MonoBehaviour {
	[SerializeField] CharacterView view;
	[SerializeField] Button dotButton;

	TestMonster me = new TestMonster();
	// Use this for initialization
	void Start () {
		view.Init (new TestMonster ());

		dotButton.onClick.AddListener (() => {
			var buff = BuffFactory.GetInstance().CreateBuff("buff_0");
			buff.CastTo(view.Character, me);
		});
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
