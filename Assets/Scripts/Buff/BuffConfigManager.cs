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
		cfg.id = "buff_0";
		cfg.duration = 2000;
		cfg.effectInterval = 500;
		cfg.effectValue = 5;
		cfg.maxStackedCount = 3;
		buffConfigs.Add (cfg.id, cfg);
	}

	public BuffConfigBase GetBuffConfig (string id) {
		if (buffConfigs.ContainsKey(id)) {
			return buffConfigs [id];
		} else {
			return null;
		}
	}
}
