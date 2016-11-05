using UnityEngine;
using System.Collections;

public interface IProcessable {
	void SetProcess(float process, bool force = false);
}
