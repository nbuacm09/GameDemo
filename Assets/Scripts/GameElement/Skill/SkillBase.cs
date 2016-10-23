using UnityEngine;
using System;
using System.Collections;

public abstract class SkillBase : FactoryObject, IEffectable {
	public SkillConfigBase SkillConfig{
		get {
			return Config as SkillConfigBase;
		}
	}
	protected CharacterBase character;
	protected CharacterBase caster;

	public BaseDelegateV<bool> onBuffRemoved;

	public ChangableDouble effectValue = new ChangableDouble();
	public ChangableInt manaCost = new ChangableInt();
	public ChangableLong cdTime = new ChangableLong();
	public long cdLeft;

	public SkillBase() {
		
	}

	public EFFECT_PROPERTY GetEffectProperty () {
		return SkillConfig.effectProperty;
	}

	public CharacterBase GetCaster() {
		return caster;
	}

	public virtual SKILL_CAST_RESULT CheckBeforeCast(CharacterBase caster, CharacterBase target) {
		return SKILL_CAST_RESULT.SUCCEED;
	}

	public override void InitWithConfig (ConfigBaseObject configObj) {
		base.InitWithConfig (configObj);
		var skillConfig = configObj as SkillConfigBase;
		Debug.Assert (skillConfig != null);

		effectValue.Set (skillConfig.effectValue);
		manaCost.Set (skillConfig.manaCost);
		cdTime.Set (skillConfig.cdTime);
	}

	public virtual void CastTo(CharacterBase character, CharacterBase caster) {
		this.character = character;
		this.caster = caster;
		OnSkillCasted ();
		Effective ();
		OnSkillOver ();
	}

	protected virtual void OnSkillCasted () {
		
	}

	protected virtual void Effective () {

	}

	protected virtual void OnSkillOver () {
		
	}
}
