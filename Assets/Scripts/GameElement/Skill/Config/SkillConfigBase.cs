using UnityEngine;
using System.Collections;

public class SkillConfigBase : ConfigBaseObject {
	public EFFECT_PROPERTY effectProperty;
	public int effectValue;
	public int manaCost;
	public long cdTime;
	public long singTime;
	public bool isBenefit;
}
