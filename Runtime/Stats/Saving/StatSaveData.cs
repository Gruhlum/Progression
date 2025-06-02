using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.Progression
{
	[System.Serializable]
	public class StatSaveData
	{
		public string name;
		public double value;

        public StatSaveData(Stat stat)
        {
			name = stat.StatData.name;
			value = stat.Value;
        }
    }
}