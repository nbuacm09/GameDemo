using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class TimeManager : MonoBehaviour
{
	static TimeManager instance = null;
	float duration = 0;
	long lastUpdateTime = 0;
	LinkedList<BaseObject> timeObjects = new LinkedList<BaseObject> ();
	HashSet<long> existedObjId = new HashSet<long> ();

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

		var it = timeObjects.First;
		while (it != null)
		{
			var cur = it;
			it = it.Next;
			var obj = cur.Value;
			if (existedObjId.Contains(obj.GetId()))
			{
				obj.Update (deltaTime);
			}
			else
			{
				timeObjects.Remove (cur);
			}
		}
	}

	public void RegistBaseObject(BaseObject obj)
	{
		if (existedObjId.Contains(obj.GetId())) {
			return;
		}
		timeObjects.AddLast (obj);
		existedObjId.Add (obj.GetId());
	}

	public void UnregistBaseObject(BaseObject obj)
	{
		existedObjId.Remove (obj.GetId());
	}
}
