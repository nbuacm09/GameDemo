using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SkillButton : MonoBehaviour {
	[SerializeField] Text nameText;
	[SerializeField] Image maskImage;
	SkillBase skill;
	public SkillBase Skill {
		get {
			return skill;
		}
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Init (SkillBase skill) {
		this.skill = skill;
		InitUI ();
	}

	void InitUI () {
		
	}
}
