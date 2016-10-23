using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProcessBar : MonoBehaviour {
	[SerializeField] bool moveAnimation = false;
	[SerializeField] float moveTime = 0.5f;

	float currentProcess;
	float targetProcess;
	float moveSpeed;
	bool moving = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (moving) {
			float nextProcess = currentProcess + moveSpeed * Time.deltaTime;
			if (moveSpeed == 0
				|| moveSpeed > 0 && nextProcess >= targetProcess
				|| moveSpeed < 0 && nextProcess <= targetProcess) {
				moving = false;
				nextProcess = targetProcess;
			}
			RealSetProcess (nextProcess);
		}
	}

	public void InitProcess(float process) {
		ForceSetPercent (process);
	}

	public void SetProcess (float process) {
		if (moveAnimation) {
			targetProcess = process;
			moveSpeed = (targetProcess - currentProcess) / moveTime;
			moving = true;
		} else {
			RealSetProcess (process);
		}
	}

	void ForceSetPercent (float process) {
		moving = false;
		RealSetProcess (process);
	}

	void RealSetProcess (float process) {
		process = MathFunc.Clamp<float> (process, 0, 1);
		gameObject.SetScaleX (process);
		currentProcess = process;
	}
}
