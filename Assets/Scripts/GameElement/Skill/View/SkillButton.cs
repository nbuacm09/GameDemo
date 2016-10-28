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
	bool inCd = false;
	long cdTime = 0;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (inCd) {
			SetCdPercent ((float)character.GetSkillCd (skill.kindId) / cdTime);
		}
	}

	void OnDestroy () {
		if (character != null) {
			character.onSkillCasted -= OnSkillCasted;
			character.onSkillCdOk -= OnSkillCdOk;
		}
	}

	public void Init (CharacterBase character, string skillKindId) {
		this.character = character;
		this.skill = SkillConfigManager.GetInstance().GetSkillConfig(skillKindId);

		this.character.onSkillCasted += OnSkillCasted;
		this.character.onSkillCdOk += OnSkillCdOk;
		InitUI ();
	}

	void InitUI () {
		nameText.text = skill.name;
		maskImage.fillAmount = 0;
	}

	void OnSkillCasted (IAbleToCastSkill caster, CharacterBase target, SkillBase skill) {
		if (skill.Config.kindId != this.skill.kindId) {
			return;
		}
		SetCd (true, skill.cdTime.Value);
	}

	void SetCd(bool val, long cdTime = 0) {
		inCd = val;
		this.cdTime = cdTime;
	}

	void OnSkillCdOk (string skillKindId) {
		if (skill.kindId != skillKindId) {
			return;
		}

		SetCd (false);
		SetCdPercent (0);
	}

	void SetCdPercent (float val) {
		maskImage.fillAmount = val;
	}
}
