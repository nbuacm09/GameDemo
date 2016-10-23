using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class CharacterBase : BaseObject {
	ChangableInt maxHp = new ChangableInt();
	int curHp;
	List<BuffBase> buffList = new List<BuffBase> ();
	public BaseDelegateV<int> onHpChanged;
	public BaseDelegateV<int> onMaxHpChanged;

	public int MaxHp{
		get{
			return maxHp.Value;
		}
	}
	public int Hp{
		get{
			return curHp;
		}
	}

	public CharacterBase() {
		maxHp.onValueChanged = OnMaxHpChanged;
		InitHp (GetInitHp ()); 
	}

	private void OnMaxHpChanged(int val) {
		SetCurHp (curHp);
		if (onMaxHpChanged != null)
		{
			onMaxHpChanged (val);
		}
	}

	public abstract int GetInitHp ();

	private void SetMaxHp(int val) {
		maxHp.Set(val);
	}

	private void SetCurHp(int val) {
		int lastHp = curHp;
		curHp = val;
		if (curHp < 0) {
			curHp = 0;
		}
		if (curHp >= maxHp.Value) {
			curHp = maxHp.Value;
		}
		if (lastHp != curHp) {
			if (onHpChanged != null) {
				onHpChanged (curHp);
			}
		}
	}

	private void InitHp(int hp) {
		SetMaxHp (hp);
		SetCurHp (hp);
	}

	public void AddBuff(BuffBase buff) {
		buffList.Add (buff);
		buff.SetCharacter (this);
	}

	public void RemoveBuff(BuffBase buff) {
		buffList.Remove (buff);
	}

	public void AddHp(int x) {
		SetCurHp (curHp + x);
	}

	public void Damage(int damageValue, IEffectable effect) {
		AddHp (-damageValue);
	}
}
