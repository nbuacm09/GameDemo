using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterView : MonoBehaviour {
	[SerializeField] Text hpText;
	int maxHp = 0;
	int hp = 0;
	CharacterBase character;
	public CharacterBase Character{
		get{
			return character;
		}
	}
	// Use this for initialization
	void Start () {

	}

	private void InitUI () {
		maxHp = character.MaxHp;
		hp = character.Hp;
		SetHpView (hp, maxHp);
	}

	private void SetHpView(int hp, int maxHp) {
		hpText.text = hp + "/" + maxHp;
	}

	public void Init (CharacterBase character) {
		this.character = character;
		InitUI ();
		this.character.onHpChanged = OnHpChanged;
		this.character.onMaxHpChanged = OnMaxHpChanged;
	}

	// Update is called once per frame
	void Update () {
	
	}

	void OnHpChanged(int hp)
	{
		this.hp = hp;
		SetHpView (hp, maxHp);
	}

	void OnMaxHpChanged(int maxHp)
	{
		this.maxHp = maxHp;
		SetHpView (hp, maxHp);
	}
}
