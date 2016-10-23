using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class CharacterBase : BaseObject {
	#region property
	Dictionary<PROPERTY, ChangableInt> propertyMax = new Dictionary<PROPERTY, ChangableInt> ();
	Dictionary<PROPERTY, int> currentProperties = new Dictionary<PROPERTY, int> ();
	public delegate void PropertyChangedDelegate (PROPERTY type, int val);
	public PropertyChangedDelegate onPropertyChanged;
	public PropertyChangedDelegate onPropertyMaxChanged;

	public ChangableInt GetPropertyMax(PROPERTY type) {
		Debug.Assert (propertyMax.ContainsKey (type));
		return propertyMax [type];
	}

	public int GetPropertyMaxValue(PROPERTY type) {
		Debug.Assert (propertyMax.ContainsKey (type));
		return propertyMax [type].Value;
	}

	public int GetProperty(PROPERTY type) {
		Debug.Assert (currentProperties.ContainsKey (type));
		return currentProperties [type];
	}

	public void SetProperty(PROPERTY type, int val) {
		Debug.Assert (currentProperties.ContainsKey (type));
		int originalVal = GetProperty (type);
		int limit = GetPropertyMaxValue (type);
		val = MathFunc.Clamp<int> (val, 0, limit);
		if (val != originalVal) {
			currentProperties [type] = val;
			if (onPropertyChanged != null) {
				onPropertyChanged (type, val);
			}
		}
	}
	public void AddProperty(PROPERTY type, int val) {
		SetProperty (type, GetProperty (type) + val);
	}

	private void OnPropertyMaxChanged(PROPERTY type, int val) {
		SetProperty (type, val);
		if (onPropertyChanged != null) {
			onPropertyChanged (type, val);
		}
	}

	void InitProperty () {
		propertyMax.Add (PROPERTY.HP, new ChangableInt(100));
		propertyMax.Add (PROPERTY.MP, new ChangableInt(1000));
		foreach (var property in propertyMax) {
			var curType = property.Key;
			property.Value.onValueChanged += (int val) => {
				OnPropertyMaxChanged (curType, val);
			};
		}

		currentProperties.Add (PROPERTY.HP, 100);
		currentProperties.Add (PROPERTY.MP, 1000);
	}

	public int MaxHp{
		get{
			return GetPropertyMaxValue(PROPERTY.HP);
		}
	}
	public int Hp{
		get{
			return GetProperty(PROPERTY.HP);
		}
	}

	public int MaxMp{
		get{
			return GetPropertyMaxValue(PROPERTY.MP);
		}
	}
	public int Mp{
		get{
			return GetProperty(PROPERTY.MP);;
		}
	}

	#endregion

	Dictionary<long, Dictionary<string, BuffBase>> buffList = new Dictionary<long, Dictionary<string, BuffBase>> ();
	public DataProcessDelegate<double> damageProcess;

	public CharacterBase() {
		InitProperty ();
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

	public void Damage(double damageValue, IEffectable effect) {
		if (damageProcess != null) {
			damageProcess (ref damageValue);
		}
		int realDamage = (int)damageValue;
		AddProperty (PROPERTY.HP, -realDamage);
	}
}
