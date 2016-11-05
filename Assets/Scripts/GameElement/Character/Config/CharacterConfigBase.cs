using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterConfigBase : ConfigBaseObject {
	public string characterType;
	public int hp;
	public int mp;
	public string ai = "";
	public List<string> skillKindIdList = new List<string> ();
	public List<string> talentKindIdList = new List<string> ();
}
