using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class CharacterBase : FactoryObject, IAbleToCastSkill {
	public CharacterConfigBase CharacterConfig{
		get {
			return Config as CharacterConfigBase;
		}
	}

	protected CharacterBase () {
		TimeManager.RegistBaseObject (this);
	}

	~CharacterBase () {
		TimeManager.UnregistBaseObject (this);
	}

	bool isDead;

	public override void Update(long deltaTime) {
		UpdateSkillCd (deltaTime);
		UpdateSkillSing (deltaTime);
	}

	public override void InitWithConfig (ConfigBaseObject configObj) {
		base.InitWithConfig (configObj);
		var characterConfig = configObj as CharacterConfigBase;
		Debug.Assert (characterConfig != null);

		InitProperty (characterConfig);
		InitLearnedSkill (characterConfig);
	}

	protected override void UnregisterAllDelegates () {
		if (target != null) {
			target.onDead -= OnTargetDead;
		}
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
	public HashSet<string> SkillList{
		get {
			return skillList;
		}
	}
	Dictionary<string, long> skillCdTimeLeftList = new Dictionary<string, long> ();
	Dictionary<string, long> skillCdTimeList = new Dictionary<string, long> ();
	public DataProcessDelegate<SkillBase> castedSkillProcess;
	public BaseDelegateV<string> onSkillLearned;
	public BaseDelegateV<string> onSkillCdOk;
	protected BaseDelegate onSkillSingOk_inner;
	public BaseDelegateV<IAbleToCastSkill, CharacterBase, SkillBase> onSkillCasted;
	public BaseDelegateV<IAbleToCastSkill, CharacterBase, SkillBase> beforeSkillCasted;

	bool isSingingSkill;
	public bool IsSingingSkill {
		get {
			return isSingingSkill;
		}
	}
	long skillSingTime;
	public long SkillSingTime {
		get {
			return skillSingTime;
		}
	}
	long skillSingTimeLeft;
	public long SkillSingTimeLeft {
		get {
			return skillSingTimeLeft;
		}
	}

	public void LearnSkill (string skillKindId) {
		skillList.Add (skillKindId);
		if (onSkillLearned != null) {
			onSkillLearned (skillKindId);
		}
	}

	void UpdateSkillSing (long deltaTime) {
		if (isSingingSkill) {
			skillSingTimeLeft -= deltaTime;
			if (skillSingTimeLeft <= 0) {
				if (onSkillSingOk_inner != null) {
					onSkillSingOk_inner ();
				}
				isSingingSkill = false;
			}
		}
	}

	void UpdateSkillCd(long deltaTime) {
		List<string> cdingSkill = new List<string> ();
		foreach (var skillCdInfo in skillCdTimeLeftList) {
			if (skillCdInfo.Value <= 0) {
				continue;
			}
			cdingSkill.Add (skillCdInfo.Key);
		}

		for (int i = 0; i < cdingSkill.Count; i++) {
			string skillKindId = cdingSkill [i];
			long cdTimeLeft = skillCdTimeLeftList [skillKindId];
			var nextVal = cdTimeLeft - deltaTime;
			if (nextVal <= 0) {
				nextVal = 0;
			}

			skillCdTimeLeftList [skillKindId] = nextVal;
			if (nextVal == 0) {
				skillCdTimeList [skillKindId] = 0;
				if (onSkillCdOk != null) {
					onSkillCdOk (skillKindId);
				}
			}
		}
	}

	void InitLearnedSkill (CharacterConfigBase config) {
		foreach (var skillKindId in config.skillKindIdList) {
			skillList.Add (skillKindId);
		}
	}

	public virtual SKILL_CAST_RESULT CreateSkill(string skillKindId, CharacterBase target, out SkillBase skill) {
		skill = null;

		if (target.isDead) {
			return SKILL_CAST_RESULT.TARGET_IS_DEAD;
		}

		if (isSingingSkill) {
			return SKILL_CAST_RESULT.IS_SINGING;
		}

		if (SkillIsLearned (skillKindId) == false) {
			return SKILL_CAST_RESULT.DISABLE;
		}

		skill = SkillFactory.GetInstance ().Create (skillKindId);
		if (castedSkillProcess != null) {
			castedSkillProcess (ref skill);
		}

		// special check by different buff.
		var buffCheckResult = skill.CheckBeforeCast (this, target);
		if (buffCheckResult != SKILL_CAST_RESULT.SUCCESS) {
			return buffCheckResult;
		}

		if (skill.manaCost.Value > Mp) {
			return SKILL_CAST_RESULT.NOT_ENOUGH_MANA;
		}

		if (GetSkillCdTimeLeft (skillKindId) > 0) {
			return SKILL_CAST_RESULT.IN_CD;
		}

		return SKILL_CAST_RESULT.SUCCESS;
	}

	public virtual SKILL_CAST_RESULT CastSkill(string skillKindId, CharacterBase target) {
		SkillBase skill;
		var skillCreateResult = CreateSkill(skillKindId, target, out skill);
		if (skillCreateResult != SKILL_CAST_RESULT.SUCCESS) {
			return skillCreateResult;
		}

		if (beforeSkillCasted != null) {
			beforeSkillCasted (this, target, skill);
		}

		SingSkill (skill, target);

		return SKILL_CAST_RESULT.SUCCESS;
	}

	protected virtual void SingSkill (SkillBase skill, CharacterBase target) {
		if (skill.singTime.Value == 0) {
			CastSkillAfterSinging(skill, target);	
		} else {
			isSingingSkill = true;
			skillSingTime = skill.singTime.Value;
			skillSingTimeLeft = skill.singTime.Value;
			onSkillSingOk_inner = () => {
				CastSkillAfterSinging(skill, target);	
			};
		}
	}

	void AddGcd () {
		foreach (var skillKindId in skillList) {
			SetSkillCd (skillKindId, 1000);
		}
	}

	protected void CastSkillAfterSinging(SkillBase skill, CharacterBase target) {
		// mana cost
		AddProperty(PROPERTY.MP, -skill.manaCost.Value);

		// cd
		SetSkillCd(skill.Config.kindId, skill.cdTime.Value);

		AddGcd ();

		if (onSkillCasted != null) {
			onSkillCasted (this, target, skill);
		}

		skill.CastTo (target, this);
	}

	public bool SkillIsLearned (string skillKindId) {
		return skillList.Contains (skillKindId);
	}

	void SetSkillCd (string skillKindId, long cdTime) {
		if (GetSkillCdTimeLeft(skillKindId) < cdTime) {
			skillCdTimeList [skillKindId] = cdTime;
			skillCdTimeLeftList [skillKindId] = cdTime;
		}
	}

	public long GetSkillCdTimeLeft (string skillKindId) {
		if (skillCdTimeLeftList.ContainsKey(skillKindId)) {
			return skillCdTimeLeftList [skillKindId];
		} else {
			return 0;
		}
	}

	public long GetSkillCdTime (string skillKindId) {
		if (skillCdTimeList.ContainsKey(skillKindId)) {
			return skillCdTimeList [skillKindId];
		} else {
			return 0;
		}
	}
	#endregion

	#region buff on body
	List<BuffBase> buffList = new List<BuffBase> ();
	public BaseDelegateV<BuffBase> onNewBuffAppended;

	public List<BuffBase> GetBuffList() {
		return buffList;
	}

	public BuffBase GetBuff (string buffKindId, IAbleToCastSkill caster) {
		foreach(var buff in buffList) {
			if (buff.Config.kindId == buffKindId && buff.Caster == caster) {
				return buff;
			}
		}
		return null;
	}

	public void AddBuff(BuffBase buff) {
		buffList.Add (buff);
		if (onNewBuffAppended != null) {
			onNewBuffAppended (buff);
		}
	}

	public void RemoveBuff(BuffBase buff) {
		buffList.Remove (buff);
	}
	#endregion

	#region target
	CharacterBase target;
	public CharacterBase Target {
		get {
			return target;
		}
	}
	public BaseDelegateV<CharacterBase, CharacterBase> onTargetSwitched;
	void OnTargetDead (SkillBase skill) {
		SelectTarget (null);
	}
	public void SelectTarget(CharacterBase newTarget) {
		if (target == newTarget) {
			return;
		}

		if (target != null) {
			target.onDead -= OnTargetDead;
		}

		CharacterBase oriTarget = target;
		target = newTarget;

		if (newTarget != null) {
			newTarget.onDead += OnTargetDead;
		}

		if (onTargetSwitched != null) {
			onTargetSwitched (oriTarget, newTarget);
		}
	}
	#endregion

	#region damage
	public DataProcessDelegate<double> damageProcess;
	public BaseDelegateV<int, SkillBase> onDamaged;
	public BaseDelegateV<SkillBase> onDead;

	public void Damage(double damageValue, SkillBase skill) {
		if (damageProcess != null) {
			damageProcess (ref damageValue);
		}
		int realDamage = (int)damageValue;
		AddProperty (PROPERTY.HP, -realDamage);
		if (onDamaged != null) {
			onDamaged (realDamage, skill);
		}

		if (GetProperty(PROPERTY.HP) <= 0) {
			SetProperty (PROPERTY.HP, 0);
			Die (skill);
		}
	}

	protected virtual void Die (SkillBase skill) {
		isDead = true;

		if (onDead != null) {
			onDead (skill);
		}
		Destroy ();
	}
	#endregion
}
