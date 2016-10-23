using UnityEngine;
using System.Collections;

public class BuffFactory : Singleton<BuffFactory> {
	
	public BuffBase CreateBuff (string id) {
		Debug.Log ("Create buff " + id);
		BuffConfigBase buffConfig = BuffConfigManager.GetInstance ().GetBuffConfig (id);
		if (buffConfig == null) {
			return null;
		}

		var buff = GetBuff (buffConfig.id);
		Debug.Log ("buff " + buff);
		if (buff != null) {
			buff.InitWithConfig (buffConfig);
		}

		return buff;
	}

	BuffBase GetBuff (string id) {
		BuffBase ret = null;
		switch (id) {
		case "buff_0":
			ret = new BuffDot ();
			break;
		}
		return ret;
	}
}
