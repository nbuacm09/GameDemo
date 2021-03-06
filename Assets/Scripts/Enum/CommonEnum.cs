﻿public enum EFFECT_TYPE {
	HEALTH,
	DAMAGE
}

public enum PROPERTY {
	HP,
	MP
}

public enum SKILL_CAST_RESULT {
	SUCCESS,
	NO_TARGET,
	NOT_ENOUGH_MANA,
	IN_CD,
	STUN,
	SILENCE,
	IS_SINGING,
	DISABLE,
	TARGET_IS_DEAD
}

public enum BATTLE_JOB {
	BOSS,
	FOLLOWER
}