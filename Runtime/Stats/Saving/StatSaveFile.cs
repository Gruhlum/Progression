using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HexTecGames.Progression
{
    [System.Serializable]
    public class StatSaveFile
    {
        public List<StatSaveData> datas = new List<StatSaveData>();

        public StatSaveFile(List<Stat> stats)
        {
            if (stats == null)
            {
                return;
            }

            foreach (Stat stat in stats)
            {
                var result = datas.Find(x => x.name == stat.StatType.name);

                if (result != null)
                {
                    result.value = stat.Value;
                }
                else datas.Add(new StatSaveData(stat));
            }
        }
        public int RetrieveValue(StatType statType, int defaultValue = 0)
        {
            StatSaveData saveData = datas.Find(x => x.name == statType.name);
            if (saveData != null)
            {
                return saveData.value;
            }
            return defaultValue;
        }
    }
}