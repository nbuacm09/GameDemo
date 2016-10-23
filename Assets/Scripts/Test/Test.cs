using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class Test : MonoBehaviour {
	[SerializeField] CharacterView view;
	[SerializeField] Button dotButton;
	// Use this for initialization
	void Start () {
		view.Init (new TestMonster ());

		dotButton.onClick.AddListener (() => {
			view.Character.AddBuff (BuffFactory.GetInstance().CreateBuff("buff_0"));
		});
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
