using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class MainUI : MonoBehaviour {
	[SerializeField] CharacterPanel characterPanel;
	[SerializeField] BuffInfoPanel buffInfoPanel;
	[SerializeField] SkillButtonPanel skillButtonPanel;
	[SerializeField] SkillSingPanel skillSingPanel;

	CharacterBase me;
	CharacterBase enemy;

	// Use this for initialization
	void Start () {
		// init me and enemy
		me = CharacterFactory.GetInstance ().Create ("character_0");
		enemy = CharacterFactory.GetInstance ().Create ("character_1");

		characterPanel.Init (me);
		me.SelectTarget (enemy);

		skillButtonPanel.Init (me);
		buffInfoPanel.Init (me);
		skillSingPanel.Init (me);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
