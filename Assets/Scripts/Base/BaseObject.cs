using UnityEngine;
using System.Collections;

public abstract class BaseObject {
	static long idCreator;
	long id;
	public long GetId () {
		return id;
	}
	bool isDestroyed;
	public bool IsDestroyed {
		get {
			return isDestroyed;
		}
	}
	public BaseDelegate onDestroy;

	public BaseObject() {
		id = idCreator++;
	}

	public void TimeManagerUpdate(long deltaTime) {
		if (IsDestroyed) {
			return;
		}
		Update (deltaTime);
	}
	protected virtual void Update(long deltaTime) {

	}

	protected virtual void Destroy () {
		UnregisterAllDelegates ();
		isDestroyed = true;
		if (onDestroy != null) {
			onDestroy ();
		}
	}

	protected virtual void UnregisterAllDelegates () {
		
	}
}

public abstract class FactoryObject : BaseObject {
	protected ConfigBaseObject config;
	public ConfigBaseObject Config {
		get {
			return config;
		}
	}
	public virtual void InitWithConfig (ConfigBaseObject configObject) {
		this.config = configObject;
	}
}