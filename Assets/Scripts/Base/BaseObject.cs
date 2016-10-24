using UnityEngine;
using System.Collections;

public abstract class BaseObject {
	static long idCreator;
	long id;
	public long GetId () {
		return id;
	}
	public BaseObject() {
		id = idCreator++;
	}

	public virtual void Update(long deltaTime) {
		
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