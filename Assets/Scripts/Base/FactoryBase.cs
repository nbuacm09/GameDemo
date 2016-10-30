using UnityEngine;
using System.Collections;

public abstract class FactoryBase<T, U> : Singleton<T> where U : FactoryObject where T : class, new () {
	public U Create (string kindId) {
		ConfigBaseObject config = GetConfig (kindId);
		if (config == null) {
			Debug.LogWarning ("Factory create an object failed [NO CONFIG] : ID = " + kindId);
			return null;
		}

		var obj = CreateObject (kindId);
		if (obj != null) {
			obj.InitWithConfig (config);
		}

		return obj;
	}

	protected abstract ConfigBaseObject GetConfig (string kindId);
	protected abstract U CreateObject (string kindId);
}
