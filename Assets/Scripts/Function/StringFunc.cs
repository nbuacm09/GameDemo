using UnityEngine;
using System;
using System.Reflection;
using System.Collections;

public static class StringFunc {
	public static string GetTimeStringMinLength (long time) {
		time = time / 1000;
		string[] unit = { "s", "m", "h", "d" };
		int[] ratio = { 60, 60, 24 };
		int curUnit = 0;
		while (curUnit + 1 < unit.Length) {
			if (time / ratio[curUnit] > 0) {
				time /= ratio [curUnit];
				curUnit++;
			} else {
				break;
			}
		}
		return time + " " + unit [curUnit];
	}
}
