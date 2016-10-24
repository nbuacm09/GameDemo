using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterConfigBase : ConfigBaseObject {
	public int hp;
	public int mp;
	public HashSet<string> skillKindIdList = new HashSet<string> ();
}
