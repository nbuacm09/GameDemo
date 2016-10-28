using UnityEngine;
using System;
using System.Collections;

public abstract class SkillBase : FactoryObject {
	public SkillConfigBase SkillConfig{
		get {
			return Config as SkillConfigBase;
		}
	}

	protected CharacterBase target;
	protected IAbleToCastSkill caster;

	public ChangableDouble effectValue = new ChangableDouble();
	public ChangableInt manaCost = new ChangableInt();
	public ChangableLong cdTime = new ChangableLong();
	public ChangableLong singTime = new ChangableLong();
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

		effectValue.Set (skillConfig.effectValue);
		manaCost.Set (skillConfig.manaCost);
		cdTime.Set (skillConfig.cdTime);
		singTime.Set (skillConfig.singTime);
	}

	public virtual void CastTo(CharacterBase target, IAbleToCastSkill caster) {
		this.target = target;
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
