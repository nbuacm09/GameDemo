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

	CharacterBase me;
	// Use this for initialization
	void Start () {
		me = CharacterFactory.GetInstance ().Create ("character_0");

		var enemy = CharacterFactory.GetInstance ().Create ("character_1");
		enemy.onNewBuffAppended += OnNewBuffAppendedOnEnemy;
		view.Init (enemy);

		dotButton.onClick.AddListener (() => {
			CastSkill("buff_0");
		});

		damageButton.onClick.AddListener (() => {
			CastSkill("buff_1");
		});

		healthBombButton.onClick.AddListener (() => {
			CastSkill("buff_2");
		});

		punchButton.onClick.AddListener (() => {
			CastSkill("skill_0");
		});
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void CastSkill (string skillKindId) {
		var res = me.CastSkill (skillKindId, view.Character);
		if (res != SKILL_CAST_RESULT.SUCCEED) {
			CastSkillWarning (res);
		}
	}

	void CastSkillWarning (SKILL_CAST_RESULT res) {
		// TODO
	}

	void OnNewBuffAppendedOnEnemy (BuffBase buff) {
		var buffObject = Instantiate (buffViewPrefab);
		buffObject.SetParent (buffPanel);
		var buffView = buffObject.GetComponent<BuffView> ();
		buffView.Init (buff);
	}
}
