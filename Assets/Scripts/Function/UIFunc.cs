using UnityEngine;
using System;
using System.Reflection;
using System.Collections;

public static class UIFunc {
	public static void SetParent (this GameObject obj, GameObject parent) {
		obj.transform.SetParent(parent.transform, false);
	}

	public static void ClearChildren (this GameObject obj) {
		for (int i = obj.transform.childCount - 1; i >= 0; i--) {
			GameObject.Destroy (obj.transform.GetChild (i).gameObject);
		}

	}

	public static void SetScaleX(this GameObject obj, double scaleX) {
		var scale = obj.transform.localScale;
		scale.x = (float)scaleX;
		obj.transform.localScale = scale;
	}

	public static void SetScaleY(this GameObject obj, double scaleY) {
		var scale = obj.transform.localScale;
		scale.y = (float)scaleY;
		obj.transform.localScale = scale;
	}
}
