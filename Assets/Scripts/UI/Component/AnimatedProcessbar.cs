using UnityEngine;
using System.Collections;

public class AnimatedProcessbar : MonoBehaviour {
	[SerializeField] ProcessBar upBar;
	[SerializeField] ProcessBar downBar;
	public void SetProcess (float percent) {
		upBar.SetProcess (percent);
		downBar.SetProcess (percent);
	}

	public void InitProcess (float percent) {
		upBar.InitProcess (percent);
		downBar.InitProcess (percent);
	}
}
