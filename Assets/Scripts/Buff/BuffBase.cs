using UnityEngine;
using System;
using System.Collections;

public abstract class BuffBase : BaseObject, IEffectable {
	protected BuffConfigBase buffConfig;
	public BuffConfigBase BuffConfig {
		get {
			return buffConfig;
		}
	}

	protected CharacterBase character;
	protected CharacterBase caster;

	public BaseDelegateV<bool> onBuffRemoved;

	public ChangableDouble effectValue = new ChangableDouble();
	public ChangableInt manaCost = new ChangableInt();

	public BuffBase() {
		
	}

	public EFFECT_PROPERTY_TYPE GetEffectProperty () {
		return buffConfig.effectProperty;
	}

	public CharacterBase GetCaster() {
		return caster;
	}

	public virtual void InitWithConfig (BuffConfigBase buffConfig) {
		this.buffConfig = buffConfig;

		effectValue.Set (buffConfig.effectValue);
		manaCost.Set (buffConfig.manaCost);
	}
	/// <summary>
	/// Casts to.
	/// </summary>
	/// <returns><c>true</c>, if this buff need a buff view on UI, <c>false</c> otherwise.</returns>
	/// <param name="character">Character.</param>
	/// <param name="caster">Caster.</param>
	public virtual bool CastTo(CharacterBase character, CharacterBase caster) {
		this.character = character;
		this.caster = caster;
		OnBuffCasted ();
		Effective ();
		OnBuffOver ();
		return false;
	}

	protected virtual void OnBuffCasted () {
		
	}

	protected virtual void Effective () {

	}

	protected virtual void OnBuffOver () {
		
	}
}
