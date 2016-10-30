using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public abstract class SkillBase : FactoryObject {
	public SkillConfigBase SkillConfig{
		get {
			return Config as SkillConfigBase;
		}
	}

	protected CharacterBase target;
	public CharacterBase Target {
		get {
			return target;
		}
	}
	protected IAbleToCastSkill caster;
	public IAbleToCastSkill Caster {
		get {
			return caster;
		}
	}

	public class CasterInfluence {
		public double damageAdd = 0;
		public double damageMulti = 1;
		public double healthAdd = 0;
		public double healthMulti = 1;
		public double singTimeMulti = 1;
		public double cdTimeMulti = 1;
		public double manaCostMulti = 1;
	}

	public CasterInfluence casterInfluence = new CasterInfluence();

	public void SetCasterAddition (CasterInfluence casterInfluence) {
		this.casterInfluence = casterInfluence;
	}

	protected void Damage (double originalDamage) {
		double damage = (originalDamage + casterInfluence.damageAdd) * casterInfluence.damageMulti;
		target.Damage (damage, this);
	}

	protected void Health (double originalHealth) {
		double health = (originalHealth + casterInfluence.healthAdd) * casterInfluence.healthMulti;
		target.Health (health, this);
	}

	public long SingTime {
		get {
			return (long)(SkillConfig.singTime * casterInfluence.singTimeMulti);
		}
	}

	public long CdTime {
		get {
			return (long)(SkillConfig.cdTime * casterInfluence.cdTimeMulti);
		}
	}

	public int ManaCost {
		get {
			return (int)(SkillConfig.manaCost * casterInfluence.manaCostMulti);
		}
	}

	public long cdLeft;

	public SkillBase() {
		
	}

	public virtual SKILL_CAST_RESULT CheckBeforeCast(CharacterBase caster, CharacterBase target) {
		return SKILL_CAST_RESULT.SUCCESS;
	}

	public override void InitWithConfig (ConfigBaseObject configObj) {
		base.InitWithConfig (configObj);
		var skillConfig = configObj as SkillConfigBase;
		Debug.Assert (skillConfig != null);
	}

	public virtual void CastTo(CharacterBase target, IAbleToCastSkill caster) {
		this.target = target;
		this.caster = caster;

		OnSkillCasted ();
		Effective ();
		Destroy ();
		OnSkillOver ();
	}

	protected virtual void OnSkillCasted () {
		
	}

	protected virtual void Effective () {
		
	}

	protected virtual void OnSkillOver () {
		
	}
}
