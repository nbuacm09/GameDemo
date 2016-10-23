using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class BuffConfigManager : Singleton<BuffConfigManager> {
	Dictionary<string, BuffConfigBase> buffConfigs = new Dictionary<string, BuffConfigBase> ();

	public BuffConfigManager () {
		Init ();
	}

	public void Init () {
		ReadConfig ();
	}

	void ReadConfig () {
		BuffConfigBase cfg = new BuffConfigBase();
		cfg.kindId = "buff_0";
		cfg.duration = 2000;
		cfg.effectInterval = 500;
		cfg.effectValue = 1;
		cfg.maxStackedCount = 3;
		buffConfigs.Add (cfg.kindId, cfg);
	}

	public BuffConfigBase GetBuffConfig (string kindId) {
		if (buffConfigs.ContainsKey(kindId)) {
			return buffConfigs [kindId];
		} else {
			return null;
		}
	}
}
