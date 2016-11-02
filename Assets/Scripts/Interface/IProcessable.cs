using UnityEngine;
using System.Collections;

public interface IProcessable {
	void SetProcess(double process, bool force = false);
}
