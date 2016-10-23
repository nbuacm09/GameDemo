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
		BuffConfigBase buffCfg;
		DurationBuffConfigBase durationBuffCfg;

		buffCfg = new BuffConfigBase ();
		buffCfg.kindId = "buff_0";
		buffCfg.name = "punch";
		buffCfg.effectValue = 10;
		buffConfigs.Add (buffCfg.kindId, buffCfg);

		durationBuffCfg = new DurationBuffConfigBase();
		durationBuffCfg.kindId = "durationbuff_0";
		durationBuffCfg.name = "dot";
		durationBuffCfg.duration = 5000;
		durationBuffCfg.effectInterval = 500;
		durationBuffCfg.effectValue = 1;
		durationBuffCfg.maxStackedCount = 3;
		buffConfigs.Add (durationBuffCfg.kindId, durationBuffCfg);

		durationBuffCfg = new DurationBuffConfigBase();
		durationBuffCfg.kindId = "durationbuff_1";
		durationBuffCfg.name = "dmg*2";
		durationBuffCfg.duration = 20000;
		durationBuffCfg.effectInterval = -1;
		durationBuffCfg.effectValue = 2;
		durationBuffCfg.maxStackedCount = 1;
		buffConfigs.Add (durationBuffCfg.kindId, durationBuffCfg);

		durationBuffCfg = new DurationBuffConfigBase();
		durationBuffCfg.kindId = "durationbuff_2";
		durationBuffCfg.name = "health bomb";
		durationBuffCfg.duration = 5000;
		durationBuffCfg.effectInterval = 500;
		durationBuffCfg.effectValue = -1;
		durationBuffCfg.maxStackedCount = 3;
		buffConfigs.Add (durationBuffCfg.kindId, durationBuffCfg);

	}

	public BuffConfigBase GetBuffConfig (string kindId) {
		if (buffConfigs.ContainsKey(kindId)) {
			return buffConfigs [kindId];
		} else {
			return null;
		}
	}
}
