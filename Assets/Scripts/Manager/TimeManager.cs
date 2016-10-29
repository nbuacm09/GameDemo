using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class TimeManager : MonoBehaviour
{
	static TimeManager instance = null;
	float duration = 0;
	long lastUpdateTime = 0;
	static Dictionary<long, WeakReference> timeObjects = new Dictionary<long, WeakReference> ();

	static public TimeManager GetInstance()
	{
		return instance;
	}

	void Awake ()
	{
		instance = this;
	}

	// Update is called once per frame
	void Update ()
	{
		duration += Time.deltaTime;
		long curTime = (long)(duration * 1000);
		long deltaTime = curTime - lastUpdateTime;
		lastUpdateTime = curTime;

		List<WeakReference> objs = new List<WeakReference> ();
		foreach (var obj in timeObjects) {
			objs.Add (obj.Value);
		}

		for (int i = 0; i < objs.Count; i++) {
			if (objs[i].IsAlive == false) {
				continue;
			}
			BaseObject obj = objs [i].Target as BaseObject;
			if (timeObjects.ContainsKey(obj.GetId()) == false) {
				continue;
			}
			obj.Update (deltaTime);
		}
	}

	public static void RegistBaseObject(BaseObject obj)
	{
		timeObjects[obj.GetId()] = new WeakReference(obj);
	}

	public static void UnregistBaseObject(BaseObject obj)
	{
		timeObjects.Remove (obj.GetId());
	}
}
