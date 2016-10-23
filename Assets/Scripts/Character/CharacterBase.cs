using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class CharacterBase : FactoryObject {
	public CharacterConfigBase CharacterConfig{
		get {
			return Config as CharacterConfigBase;
		}
	}

	public CharacterBase() {
		InitProperty ();
		InitLearnedBuff ();
	}

	public override void InitWithConfig (ConfigBaseObject configObj) {
		base.InitWithConfig (configObj);
		var characterConfig = configObj as CharacterConfigBase;
		Debug.Assert (characterConfig != null);

		// TODO
	}

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
		// TODO: read from config
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

	#region skill
	HashSet<string> skillList = new HashSet<string> ();
	Dictionary<string, long> skillCdTime = new Dictionary<string, long> ();
	public DataProcessDelegate<SkillBase> castedSkillProcess;
	public BaseDelegateV<string> onSkillLearned;
	public void LearnBuff (string buffKindId) {
		skillList.Add (buffKindId);
		if (onSkillLearned != null) {
			onSkillLearned (buffKindId);
		}
	}

	void InitLearnedBuff () {
		// TODO: read from config
		skillList.Add ("buff_0");
		skillList.Add ("buff_1");
		skillList.Add ("buff_2");

		skillList.Add ("skill_0");
	}

	public SKILL_CAST_RESULT CastSkill(string skillKindId, CharacterBase target) {
		#region check if I can cast skill
		if (SkillIsLearned (skillKindId) == false) {
			return SKILL_CAST_RESULT.DISABLE;
		}

		var skill = SkillFactory.GetInstance ().Create (skillKindId);
		if (castedSkillProcess != null) {
			castedSkillProcess (ref skill);
		}

		// special check by different buff.
		var buffCheckResult = skill.CheckBeforeCast (this, target);
		if (buffCheckResult != SKILL_CAST_RESULT.SUCCEED) {
			return buffCheckResult;
		}

		if (skill.manaCost.Value > Mp) {
			return SKILL_CAST_RESULT.NOT_ENOUGH_MANA;
		}

		if (GetSkillCd (skillKindId) > 0) {
			return SKILL_CAST_RESULT.IN_CD;
		}
		#endregion

		skill.CastTo (target, this);

		return SKILL_CAST_RESULT.SUCCEED;
	}

	public bool SkillIsLearned (string skillKindId) {
		return skillList.Contains (skillKindId);
	}

	public long GetSkillCd (string skillKindId) {
		if (skillCdTime.ContainsKey(skillKindId)) {
			return skillCdTime [skillKindId];
		} else {
			return 0;
		}
	}
	#endregion

	#region buff on body
	Dictionary<long, Dictionary<string, SkillBase>> buffList = new Dictionary<long, Dictionary<string, SkillBase>> ();
	public BaseDelegateV<BuffBase> onNewBuffAppended;
	public SkillBase GetBuff (string buffKindId, CharacterBase caster) {
		if (buffList.ContainsKey(caster.Id) == false) {
			return null;
		}

		var list = buffList [caster.Id];
		if (list.ContainsKey(buffKindId) == false) {
			return null;
		}

		return list [buffKindId];
	}

	Dictionary<string, SkillBase> GetBuffList(long casterId) {
		if (buffList.ContainsKey(casterId) == false) {
			buffList.Add (casterId, new Dictionary<string, SkillBase> ());
		}
		return buffList [casterId];
	}

	public void AddBuff(BuffBase buff, CharacterBase caster) {
		GetBuffList (caster.Id).Add (buff.SkillConfig.kindId, buff);
		if (onNewBuffAppended != null) {
			onNewBuffAppended (buff);
		}
	}

	public void RemoveBuff(SkillBase buff, CharacterBase caster) {
		if (buffList.ContainsKey(caster.Id) == false) {
			return;
		}
		var list = buffList [caster.Id];
		if (list.ContainsKey(buff.SkillConfig.kindId)) {
			list.Remove (buff.SkillConfig.kindId);
			if (list.Count == 0) {
				buffList.Remove (caster.Id);
			}
		}
	}
	#endregion

	#region damage
	public DataProcessDelegate<double> damageProcess;

	public void Damage(double damageValue, IEffectable effect) {
		if (damageProcess != null) {
			damageProcess (ref damageValue);
		}
		int realDamage = (int)damageValue;
		AddProperty (PROPERTY.HP, -realDamage);
	}
	#endregion
}
