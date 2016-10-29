using UnityEngine;
using System.Collections;

public class AnimatedProcessbar : MonoBehaviour, IProcessable {
	[SerializeField] ProcessBar upBar;
	[SerializeField] ProcessBar downBar;
	public void SetProcess (float percent, bool force = false) {
		upBar.SetProcess (percent, force);
		downBar.SetProcess (percent, force);
	}

	public void InitProcess (float percent) {
		upBar.InitProcess (percent);
		downBar.InitProcess (percent);
	}
}
