using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterInfoView : MonoBehaviour {
	[SerializeField] Text hpText;
	[SerializeField] AnimatedProcessbar hpBar;
	[SerializeField] Text mpText;
	[SerializeField] AnimatedProcessbar mpBar;
	int maxHp = 0;
	int hp = 0;
	int maxMp = 0;
	int mp = 0;
	CharacterBase character;
	public CharacterBase Character{
		get{
			return character;
		}
	}
	// Use this for initialization
	void Start () {

	}

	protected virtual void OnDestroy () {
		if (character != null) {
			this.character.onPropertyChanged -= OnPropertyChanged;
			this.character.onPropertyMaxChanged -= OnPropertyMaxChanged;
		}
	}

	// Update is called once per frame
	void Update () {

	}

	private void InitUI () {
		maxHp = character.MaxHp;
		hp = character.Hp;
		hpBar.InitProcess (1);
		SetHpView (hp, maxHp);

		maxMp = character.MaxMp;
		mp = character.Mp;
		mpBar.InitProcess (1);
		SetMpView (mp, maxMp);
	}

	private void SetHpView(int hp, int maxHp) {
		hpText.text = hp + "/" + maxHp;
		hpBar.SetProcess ((float)hp / maxHp);
	}

	private void SetMpView(int mp, int maxMp) {
		mpText.text = mp + "/" + maxMp;
		mpBar.SetProcess ((float)mp / maxMp);
	}

	public void Init (CharacterBase character) {
		this.character = character;
		InitUI ();
		this.character.onPropertyChanged += OnPropertyChanged;
		this.character.onPropertyMaxChanged += OnPropertyMaxChanged;
	}

	void OnPropertyChanged(PROPERTY type, int val) {
		switch (type) {
		case PROPERTY.HP:
			this.hp = val;
			SetHpView (hp, maxHp);
			break;
		case PROPERTY.MP:
			this.mp = val;
			SetMpView (mp, maxMp);
			break;
		}
	}

	void OnPropertyMaxChanged(PROPERTY type, int val)
	{
		switch (type) {
		case PROPERTY.HP:
			this.maxHp = val;
			SetHpView (hp, maxHp);
			break;
		case PROPERTY.MP:
			this.maxMp = val;
			SetMpView (mp, maxMp);
			break;
		}
	}
}
