using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class GameTimeManager : MonoBehaviour
{
	static GameTimeManager instance = null;
	double duration = 0;
	long lastUpdateTime = 0;
	static Dictionary<long, WeakReference> timeObjects = new Dictionary<long, WeakReference> ();

	bool isGaming = false;
	public bool IsGaming {
		get {
			return isGaming;
		}
	}
	double gameSpeed = 1;
	public double GameSpeed {
		get {
			return gameSpeed;
		}
	}

	static public GameTimeManager GetInstance()
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
		if (isGaming == false) {
			return;
		}
		duration += Time.deltaTime * gameSpeed;
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
			if (timeObjects.ContainsKey(obj.Id) == false) {
				continue;
			}
			obj.TimeManagerUpdate (deltaTime);
		}
	}

	public static void RegistBaseObject(BaseObject obj)
	{
		timeObjects[obj.Id] = new WeakReference(obj);
	}

	public static void UnregistBaseObject(BaseObject obj)
	{
		timeObjects.Remove (obj.Id);
	}

	public void Start () {
		isGaming = true;
	}

	public void Stop () {
		isGaming = false;
	}

	public void SetSpeed (double speed) {
		gameSpeed = speed;
	}
}
