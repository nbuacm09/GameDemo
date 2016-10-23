using UnityEngine;
using System.Collections;


public class Singleton<T> where T : class, new ()
{
	static T instance = null;
	static public T GetInstance()
	{
		if (instance == null)
		{
			instance = new T ();
		}
		return instance;
	}
}
