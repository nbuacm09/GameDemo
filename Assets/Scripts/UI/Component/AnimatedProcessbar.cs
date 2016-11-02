using UnityEngine;
using System.Collections;

public class AnimatedProcessbar : MonoBehaviour, IProcessable {
	[SerializeField] ProcessBar upBar;
	[SerializeField] ProcessBar downBar;
	public void SetProcess (double percent, bool force = false) {
		upBar.SetProcess (percent, force);
		downBar.SetProcess (percent, force);
	}

	public void InitProcess (double percent) {
		upBar.InitProcess (percent);
		downBar.InitProcess (percent);
	}
}
