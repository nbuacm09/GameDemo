using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Entry : MonoBehaviour {
	[SerializeField] Button newGameButton;
	[SerializeField] Button loadGameButton;

	// Use this for initialization
	void Start () {
		newGameButton.onClick.AddListener (NewGame);
		loadGameButton.onClick.AddListener (LoadGame);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void LoadGame () {
		GameManager.GetInstance ().LoadGame ();
		SceneManager.LoadScene ("Battle");
	}

	void NewGame () {
		GameManager.GetInstance ().NewGame ();
		SceneManager.LoadScene ("Battle");
	}
}
