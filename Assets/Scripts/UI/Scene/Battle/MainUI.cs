using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class MainUI : MonoBehaviour {
	[SerializeField] BattlePanel battlePanel;

	// Use this for initialization
	void Start () {
		var battle = GameManager.GetInstance ().CurrentBattle;
		battlePanel.Init (battle);
	}
}
