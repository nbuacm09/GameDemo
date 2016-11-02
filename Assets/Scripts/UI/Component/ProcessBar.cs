using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProcessBar : MonoBehaviour, IProcessable {
	[SerializeField] bool moveAnimation = false;
	[SerializeField] double moveTime = 0.5;
	[SerializeField] GameObject bar;

	double currentProcess;
	double targetProcess;
	double moveSpeed;
	bool moving = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (moving) {
			double nextProcess = currentProcess + moveSpeed * Time.deltaTime;
			if (moveSpeed == 0
				|| moveSpeed > 0 && nextProcess >= targetProcess
				|| moveSpeed < 0 && nextProcess <= targetProcess) {
				moving = false;
				nextProcess = targetProcess;
			}
			RealSetProcess (nextProcess);
		}
	}

	public void InitProcess(double process) {
		SetProcess (process, true);
	}

	public void SetProcess (double process, bool force = false) {
		if (force) {
			moving = false;
			RealSetProcess (process);
		} else {
			if (moveAnimation) {
				targetProcess = process;
				moveSpeed = (targetProcess - currentProcess) / moveTime;
				moving = true;
			} else {
				RealSetProcess (process);
			}
		}
	}

	void RealSetProcess (double process) {
		process = MathFunc.Clamp<double> (process, 0, 1);
		GameObject realBar = bar == null ? gameObject : bar;
		realBar.SetScaleX (process);
		currentProcess = process;
	}
}
