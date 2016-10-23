using UnityEngine;
using System;
using System.Reflection;
using System.Collections;

public static class MathFunc {
	public static T Clamp<T> (T val, T minVal, T maxVal) where T : IComparable {
		Debug.Assert (minVal.CompareTo(maxVal) <= 0);
		if (val.CompareTo (minVal) < 0) {
			val = minVal;
		}

		if (val.CompareTo (maxVal) > 0) {
			val = maxVal;
		}
		return val;
	}
}
