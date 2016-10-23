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
	long latestEffectTime;
	public long LatestEffectTime {
		get {
			return latestEffectTime;
		}
	}
	long effectTimes;
	public long EffectTimes {
		get {
			return effectTimes;
		}
	}
	long passedTime;
	public long PassedTime{
		get{
			return passedTime;
		}
	}
	long timeLeft;
	public long TimeLeft{
		get{
			return timeLeft;
		}
	}
	int stackedCount;
	public int StackedCount{
		get{
			return stackedCount;
		}
	}

	protected ChangableLong effectInterval = new ChangableLong();
	protected ChangableLong duration = new ChangableLong();
	protected ChangableInt maxStackedCount = new ChangableInt();
	protected ChangableInt effectValue = new ChangableInt();

	public BuffBase() {
		latestEffectTime = -1;
		stackedCount = 1;
		effectTimes = 0;
		passedTime = 0;
	}

	public virtual void InitWithConfig (BuffConfigBase buffConfig) {
		this.buffConfig = buffConfig;

		timeLeft = buffConfig.duration;
		effectInterval.Set (buffConfig.effectInterval);
		duration.Set (buffConfig.duration);
		maxStackedCount.Set (buffConfig.maxStackedCount);
		effectValue.Set (buffConfig.effectValue);
	}

	public void CastTo(CharacterBase character, CharacterBase caster) {
		this.character = character;
		this.caster = caster;
		var characterBuff = character.GetBuff (buffConfig.kindId, caster);
		if (characterBuff == null) {
			character.AddBuff (this, caster);
			TimeManager.GetInstance ().RegistBaseObject (this);
		} else {
			characterBuff.AddStacked ();
		}
	}

	protected virtual void AddStacked () {
		if (stackedCount + 1 <= maxStackedCount.Value) {
			stackedCount++;
		}

		RefreshTimeLeft ();
	}

	void RefreshTimeLeft () {
		timeLeft = duration.Value;
	}

	public DAMAGE_SOURCE_TYPE GetDamageSourceType() {
		return DAMAGE_SOURCE_TYPE.BUFF;
	}
	public CharacterBase GetCaster() {
		return caster;
	}

	public override void Update(long deltaTime) {
		passedTime += deltaTime;
		timeLeft -= deltaTime;

		int currentEffectTimes = TryEffect (ref latestEffectTime);
		if (currentEffectTimes > 0) {
			effectTimes += currentEffectTimes;
		}

		if (CheckBuffOver()) {
			OnBuffOver ();
			RemoveBuff ();
		}
	}

	protected virtual void OnBuffOver () {
		
	}

	public void RemoveBuff () {
		character.RemoveBuff (this, caster);
		TimeManager.GetInstance ().UnregistBaseObject (this);
	}

	protected virtual bool CheckBuffOver () {
		return timeLeft <= 0;
	}

	protected virtual int TryEffect(ref long latestEffectTime) {
		if (latestEffectTime < 0) {
			latestEffectTime = 0;
		}
		int effectTimes = 0;

		Debug.Assert (effectInterval.Value > 0);
		Debug.Assert ((PassedTime - latestEffectTime) / effectInterval.Value < 100);

		while(latestEffectTime + effectInterval.Value <= PassedTime) {
			long interval = effectInterval.Value;
			Effective ();
			latestEffectTime += interval;
			effectTimes++;
		}

		return effectTimes;
	}

	protected virtual void Effective () {
	
	}

	public long SetTimeLeft(long val) {
		this.timeLeft = val;
		return timeLeft;
	}

	public long AddTimeLeft(long val) {
		SetTimeLeft (timeLeft + val);
		return timeLeft;
	}
}
