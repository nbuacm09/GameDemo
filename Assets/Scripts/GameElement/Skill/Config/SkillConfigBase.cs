using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillConfigBase : ConfigBaseObject {
	public string skillType;
	public List<int> intArgs = new List<int> ();
	public List<string> stringArgs = new List<string> ();
	public int manaCost = 0;
	public long cdTime = 0;
	public long singTime = 0;
	public bool canTargetEnemy = true;
	public bool canTargetFriend = false;
	public bool NeedTarget {
		get {
			return true;
		}
	}
	public bool IsBenefit {
		get {
			return canTargetFriend && !canTargetEnemy;
		}
	}
}
