using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.Progression
{
	[System.Serializable]
	public class StatSaveFile
	{
		public List<StatSaveData> datas = new List<StatSaveData>();

		public StatSaveFile(List<Stat> stats)
		{
			foreach (var stat in stats)
			{
				datas.Add(new StatSaveData(stat));
			}
		}
		public double RetrieveValue(StatData data, double defaultValue = 0)
		{
			StatSaveData saveData = datas.Find(x => x.name == data.name);
			if (saveData != null)
			{
				return saveData.value;
			}
			return defaultValue;
		}
	}
}