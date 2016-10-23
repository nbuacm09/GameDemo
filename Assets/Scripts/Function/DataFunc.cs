using UnityEngine;
using System;
using System.Reflection;
using System.Collections;

public static class DataFunc {
	public static T DeepCopy<T> (T obj) {
		if (obj is string || obj.GetType ().IsValueType) {
			return obj;
		}
	
		object retval = Activator.CreateInstance(obj.GetType());
		FieldInfo[] fields = obj.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
		foreach (FieldInfo field in fields)
		{
			try {
				field.SetValue(retval, DeepCopy(field.GetValue(obj))); 
			} catch { 

			}
		}
		return (T)retval;
	}
}
