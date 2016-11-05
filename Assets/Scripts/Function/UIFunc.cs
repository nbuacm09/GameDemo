using UnityEngine;
using UnityEngine.UI;
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

	public static void SetScaleX(this GameObject obj, float scaleX) {
		var scale = obj.transform.localScale;
		scale.x = scaleX;
		obj.transform.localScale = scale;
	}

	public static void SetScaleY(this GameObject obj, float scaleY) {
		var scale = obj.transform.localScale;
		scale.y = scaleY;
		obj.transform.localScale = scale;
	}

	public static void SetScale(this GameObject obj, float scaleXY) {
		var scale = obj.transform.localScale;
		scale.x = scale.y = scaleXY;
		obj.transform.localScale = scale;
	}

	public static void SetAlpha(this GameObject obj, float alpha) {
		CanvasGroup cg = obj.GetComponent<CanvasGroup> ();
		if (cg != null) {
			cg.alpha = alpha;
			return;
		}
		var graph = obj.GetComponent<MaskableGraphic> ();
		if (graph != null) {
			var color = graph.color;
			color.a = alpha;
			graph.color = color;
			return;
		}
	}
}
