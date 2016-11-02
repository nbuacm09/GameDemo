using UnityEngine;
using System;
using System.Collections;

public abstract class BuffBase : SkillBase {
	public BuffConfigBase BuffConfig{
		get {
			return Config as BuffConfigBase;
		}
	}

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

	public double TimeLeftPercent {
		get {
			if (IsEndless) {
				return 0;
			} else {
				return (double)PassedTime / (TimeLeft + PassedTime);
			}
		}
	}

	int stackedCount;
	public int StackedCount{
		get{
			return stackedCount;
		}
	}

	public bool IsEndless {
		get {
			return BuffConfig.isEndless;
		}
	}

	public BaseDelegateV<long> onTimeLeftChanged;
	public BaseDelegateV<int> onStackedCountChanged;
	public BaseDelegateV<bool> onBuffRemoved;

	public ChangableLong effectInterval = new ChangableLong();
	public ChangableLong duration = new ChangableLong();
	public ChangableInt maxStackedCount = new ChangableInt();

	public BuffBase() {
		latestEffectTime = -1;
		stackedCount = 1;
		effectTimes = 0;
		passedTime = 0;
	}

	public override void InitWithConfig (ConfigBaseObject configObj) {
		base.InitWithConfig (configObj);
		var buffConfig = configObj as BuffConfigBase;
		Debug.Assert (buffConfig != null);
		var durationBuffConfig = buffConfig as BuffConfigBase;
		Debug.Assert (durationBuffConfig != null);
		timeLeft = durationBuffConfig.duration;
		effectInterval.Set (durationBuffConfig.effectInterval);
		duration.Set (durationBuffConfig.duration);
		maxStackedCount.Set (durationBuffConfig.maxStackedCount);
	}

	public override void CastTo(IAbleToCastSkill caster, CharacterBase target) {
		this.target = target;
		this.caster = caster;
		OnSkillCasted ();
		var characterBuff = target.GetBuff (BuffConfig.kindId, caster) as BuffBase;
		if (characterBuff == null) {
			target.AddBuff (this);
			target.onDead += OnTargetDead;
			GameTimeManager.RegistBaseObject (this);
			OnBuffPastedOnCharacter ();
		} else {
			characterBuff.AddStacked ();
		}
	}

	void OnTargetDead (SkillBase skill) {
		Remove ();
	}

	protected virtual void OnBuffPastedOnCharacter () {
		
	}

	protected virtual void AddStacked () {
		if (stackedCount + 1 <= maxStackedCount.Value) {
			stackedCount++;
			if (onStackedCountChanged != null) {
				onStackedCountChanged (stackedCount);
			}
		}

		RefreshTimeLeft ();
	}

	void RefreshTimeLeft () {
		passedTime = 0;
		latestEffectTime = 0;
		SetTimeLeft (duration.Value);
	}

	protected override void Update(long deltaTime) {
		passedTime += deltaTime;
		AddTimeLeft (-deltaTime);

		if (IsEffectiveByTime ()) {
			int currentEffectTimes = TryEffect (ref latestEffectTime);
			if (currentEffectTimes > 0) {
				effectTimes += currentEffectTimes;
			}
		}

		if (CheckBuffOver()) {
			Remove (true);
			OnSkillOver ();
		}
	}

	bool IsEffectiveByTime () {
		return effectInterval.Value > 0;
	}

	protected override void UnregisterAllDelegates () {
		base.UnregisterAllDelegates ();
		if (target != null) {
			target.onDead -= OnTargetDead;
		}
		GameTimeManager.UnregistBaseObject (this);
	}

	void Remove (bool timeOver) {
		target.RemoveBuff (this);
		Destroy ();
		if (onBuffRemoved != null) {
			onBuffRemoved (timeOver);
		}
	}

	public void Remove () {
		Remove (false);
	}

	protected virtual bool CheckBuffOver () {
		if (IsEndless) {
			return false;
		} else {
			return timeLeft <= 0;
		}
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

	public void SetTimeLeft(long val) {
		if (IsEndless) {
			return;
		}
		timeLeft = val;
		if (onTimeLeftChanged != null) {
			onTimeLeftChanged (timeLeft);
		}
	}

	public long AddTimeLeft(long val) {
		SetTimeLeft (timeLeft + val);
		return timeLeft;
	}
}
