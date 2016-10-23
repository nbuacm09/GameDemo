using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class Test : MonoBehaviour {
	[SerializeField] CharacterView view;
	[SerializeField] Button dotButton;
	[SerializeField] Button damageButton;
	[SerializeField] Button punchButton;
	[SerializeField] Button healthBombButton;
	[SerializeField] GameObject buffPanel;
	[SerializeField] GameObject buffViewPrefab;

	TestMonster me = new TestMonster();
	// Use this for initialization
	void Start () {
		view.Init (new TestMonster ());

		dotButton.onClick.AddListener (() => {
			CreateBuff("durationbuff_0");
		});

		damageButton.onClick.AddListener (() => {
			CreateBuff("durationbuff_1");
		});

		healthBombButton.onClick.AddListener (() => {
			CreateBuff("durationbuff_2");
		});

		punchButton.onClick.AddListener (() => {
			CreateBuff("buff_0");
		});
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void CreateBuff (string buffKindId) {
		var buff = BuffFactory.GetInstance().CreateBuff(buffKindId);
		if (buff.CastTo(view.Character, me)) {
			var durationBuff = buff as DurationBuffBase;
			var obj = Instantiate(buffViewPrefab);
			var objView = obj.GetComponent<DurationBuffView> ();
			objView.Init(durationBuff);
			obj.SetParent(buffPanel);
		}
	}
}
