using UnityEngine;
using System.Collections;

public class BuffFactory : Singleton<BuffFactory> {
	
	public BuffBase CreateBuff (string kindId) {
		Debug.Log ("Create buff: ID = " + kindId);
		BuffConfigBase buffConfig = BuffConfigManager.GetInstance ().GetBuffConfig (kindId);
		if (buffConfig == null) {
			return null;
		}

		var buff = GetBuff (buffConfig.kindId);
		Debug.Log ("Create buff: name = " + buff);
		if (buff != null) {
			buff.InitWithConfig (buffConfig);
		}

		return buff;
	}

	BuffBase GetBuff (string kindId) {
		BuffBase ret = null;
		switch (kindId) {
		case "buff_0":
			ret = new BuffPunch ();
			break;


		case "durationbuff_0":
			ret = new DurationBuffDot ();
			break;
		case "durationbuff_1":
			ret = new DurationBuffEnhanceDamage ();
			break;
		case "durationbuff_2":
			ret = new DurationBuffBombAfterHealth ();
			break;
		}
		return ret;
	}
}
