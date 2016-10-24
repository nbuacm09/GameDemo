using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class CharacterBase : FactoryObject, IAbleToCastSkill {
	public CharacterConfigBase CharacterConfig{
		get {
			return Config as CharacterConfigBase;
		}
	}

	public override void Update(long deltaTime) {
		UpdateSkillCd (deltaTime);
	}

	public override void InitWithConfig (ConfigBaseObject configObj) {
		base.InitWithConfig (configObj);
		var characterConfig = configObj as CharacterConfigBase;
		Debug.Assert (characterConfig != null);

		InitProperty (characterConfig);
		InitLearnedSkill (characterConfig);
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

	void InitProperty (CharacterConfigBase config) {
		propertyMax.Add (PROPERTY.HP, new ChangableInt(config.hp));
		propertyMax.Add (PROPERTY.MP, new ChangableInt(config.mp));

		currentProperties.Add (PROPERTY.HP, GetPropertyMaxValue(PROPERTY.HP));
		currentProperties.Add (PROPERTY.MP, GetPropertyMaxValue(PROPERTY.MP));

		// delegate set after init.
		foreach (var property in propertyMax) {
			var curType = property.Key;
			property.Value.onValueChanged += (int val) => {
				OnPropertyMaxChanged (curType, val);
			};
		}
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
	Dictionary<string, long> skillCdTimeList = new Dictionary<string, long> ();
	public DataProcessDelegate<SkillBase> castedSkillProcess;
	public BaseDelegateV<string> onSkillLearned;
	public BaseDelegateV<string> onSkillCdOk;
	public BaseDelegateV<IAbleToCastSkill, CharacterBase, SkillBase> onSkillCasted;
	public void LearnSkill (string skillKindId) {
		skillList.Add (skillKindId);
		if (onSkillLearned != null) {
			onSkillLearned (skillKindId);
		}
	}

	public void UpdateSkillCd(long deltaTime) {
		foreach (var skillCdInfo in skillCdTimeList) {
			if (skillCdInfo.Value <= 0) {
				continue;
			}

			var nextVal = skillCdInfo.Value - deltaTime;
			if (nextVal <= 0) {
				nextVal = 0;
			}

			skillCdTimeList [skillCdInfo.Key] = nextVal;
			if (nextVal == 0) {
				if (onSkillCdOk != null) {
					onSkillCdOk (skillCdInfo.Key);
				}
			}
		}
	}

	void InitLearnedSkill (CharacterConfigBase config) {
		foreach (var skillKindId in config.skillKindIdList) {
			skillList.Add (skillKindId);
		}
	}

	public virtual SKILL_CAST_RESULT CastSkill(string skillKindId, CharacterBase target) {
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

		#region skill cost
		// mana cost
		AddProperty(PROPERTY.MP, -skill.manaCost.Value);

		// cd
		SetSkillCd(skill.Config.kindId, skill.cdTime.Value);

		#endregion

		if (onSkillCasted != null) {
			onSkillCasted (this, target, skill);
		}

		skill.CastTo (target, this);

		return SKILL_CAST_RESULT.SUCCEED;
	}

	public bool SkillIsLearned (string skillKindId) {
		return skillList.Contains (skillKindId);
	}

	void SetSkillCd (string skillKindId, long cdTime) {
		skillCdTimeList [skillKindId] = cdTime;
	}

	public long GetSkillCd (string skillKindId) {
		if (skillCdTimeList.ContainsKey(skillKindId)) {
			return skillCdTimeList [skillKindId];
		} else {
			return 0;
		}
	}
	#endregion

	#region buff on body
	Dictionary<long, Dictionary<string, SkillBase>> buffList = new Dictionary<long, Dictionary<string, SkillBase>> ();
	public BaseDelegateV<BuffBase> onNewBuffAppended;
	public SkillBase GetBuff (string buffKindId, IAbleToCastSkill caster) {
		if (buffList.ContainsKey(caster.GetId()) == false) {
			return null;
		}

		var list = buffList [caster.GetId()];
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

	public void AddBuff(BuffBase buff, IAbleToCastSkill caster) {
		GetBuffList (caster.GetId()).Add (buff.SkillConfig.kindId, buff);
		if (onNewBuffAppended != null) {
			onNewBuffAppended (buff);
		}
	}

	public void RemoveBuff(SkillBase buff, IAbleToCastSkill caster) {
		if (buffList.ContainsKey(caster.GetId()) == false) {
			return;
		}
		var list = buffList [caster.GetId()];
		if (list.ContainsKey(buff.SkillConfig.kindId)) {
			list.Remove (buff.SkillConfig.kindId);
			if (list.Count == 0) {
				buffList.Remove (caster.GetId());
			}
		}
	}
	#endregion

	#region damage
	public DataProcessDelegate<double> damageProcess;
	public BaseDelegateV<int, SkillBase> onDamaged;

	public void Damage(double damageValue, SkillBase skill) {
		if (damageProcess != null) {
			damageProcess (ref damageValue);
		}
		int realDamage = (int)damageValue;
		AddProperty (PROPERTY.HP, -realDamage);
		if (onDamaged != null) {
			onDamaged (realDamage, skill);
		}
	}
	#endregion
}
