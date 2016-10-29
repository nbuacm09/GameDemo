using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SkillButton : MonoBehaviour {
	[SerializeField] Text nameText;
	[SerializeField] Image maskImage;
	CharacterBase character;
	SkillConfigBase skill;
	public SkillConfigBase Skill {
		get {
			return skill;
		}
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		long cdTime = character.GetSkillCdTime (skill.kindId);
		if (cdTime == 0) {
			SetCdPercent (0);
		} else {
			SetCdPercent ((float)character.GetSkillCdTimeLeft (skill.kindId) / cdTime);
		}
	}

	void OnDestroy () {
		if (character != null) {
			character.onSkillCdOk -= OnSkillCdOk;
		}
	}

	public void Init (CharacterBase character, string skillKindId) {
		this.character = character;
		this.skill = SkillConfigManager.GetInstance().GetSkillConfig(skillKindId);

		this.character.onSkillCdOk += OnSkillCdOk;
		InitUI ();
	}

	void InitUI () {
		nameText.text = skill.name;
		maskImage.fillAmount = 0;
	}

	void OnSkillCdOk (string skillKindId) {
		if (skill.kindId != skillKindId) {
			return;
		}

		SetCdPercent (0);
	}

	void SetCdPercent (float val) {
		maskImage.fillAmount = val;
	}
}
