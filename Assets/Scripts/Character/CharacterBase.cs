using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class CharacterBase : BaseObject {
	ChangableInt maxHp = new ChangableInt();
	int curHp;
	Dictionary<long, Dictionary<string, BuffBase>> buffList = new Dictionary<long, Dictionary<string, BuffBase>> ();
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

	public BuffBase GetBuff (string buffKindId, CharacterBase caster) {
		if (buffList.ContainsKey(caster.Id) == false) {
			return null;
		}

		var list = buffList [caster.Id];
		if (list.ContainsKey(buffKindId) == false) {
			return null;
		}

		return list [buffKindId];
	}

	Dictionary<string, BuffBase> GetBuffList(long casterId) {
		if (buffList.ContainsKey(casterId) == false) {
			buffList.Add (casterId, new Dictionary<string, BuffBase> ());
		}
		return buffList [casterId];
	}

	public void AddBuff(BuffBase buff, CharacterBase caster) {
		GetBuffList (caster.Id).Add (buff.BuffConfig.kindId, buff);
	}

	public void RemoveBuff(BuffBase buff, CharacterBase caster) {
		if (buffList.ContainsKey(caster.Id) == false) {
			return;
		}
		var list = buffList [caster.Id];
		if (list.ContainsKey(buff.BuffConfig.kindId)) {
			list.Remove (buff.BuffConfig.kindId);
			if (list.Count == 0) {
				buffList.Remove (caster.Id);
			}
		}
	}

	public void AddHp(int x) {
		SetCurHp (curHp + x);
	}

	public void Damage(int damageValue, IEffectable effect) {
		AddHp (-damageValue);
	}
}
