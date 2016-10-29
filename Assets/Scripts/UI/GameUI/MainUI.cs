using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class MainUI : MonoBehaviour {
	[SerializeField] List<CharacterInfoUIBase> characterInfoPanel;
	[SerializeField] BattlePanel battlePanel;

	// Use this for initialization
	void Start () {
		var battle = GameManager.GetInstance ().CurrentBattle;
		var player = battle.GetPlayer ();
		for (int i = 0; i < characterInfoPanel.Count; i++) {
			characterInfoPanel [i].SetCharacter (player);
		}
		battlePanel.Init (battle);
	}
}
