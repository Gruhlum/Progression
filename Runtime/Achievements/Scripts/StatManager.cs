using HexTecGames.Basics;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.Progression
{
	public class StatManager : MonoBehaviour
	{
		[SerializeField] private AchievementManager achievementsManager = default;
		[SerializeField] private List<StatData> statDatas = default;

        private List<Stat> stats = new List<Stat>();

		private const string saveFolderName = "Stats";

        void Awake()
        {
			LoadData();
        }
        void OnDestroy()
        {
			SaveData();
        }
        private void LoadData()
		{
			StatSaveFile saveFile = SaveSystem.LoadJSON<StatSaveFile>(saveFolderName);
			if (saveFile == null)
			{
				GenerateStats();
			}
			else GenerateStats(saveFile);
        }
		public void SaveData()
		{
			StatSaveFile saveFile = new StatSaveFile(stats);
			SaveSystem.SaveJSON(saveFile, saveFolderName);
		}
		private void GenerateStats()
		{
            foreach (var statData in statDatas)
            {
                stats.Add(new Stat(statData, 0));
            }
        }
		private void GenerateStats(StatSaveFile saveFile)
		{
			if (saveFile == null)
			{
				GenerateStats();
				return;
            }
            foreach (var statData in statDatas)
            {
                stats.Add(new Stat(statData, saveFile.RetrieveValue(statData)));
            }
        }
		private Achievement[] GetStatsAchievements(StatData statData)
		{
			return achievementsManager.GetStatsAchievements(statData);
		}
		public void IncreaseStat(double value, StatData statData)
		{
			Stat stat = stats.Find(x => x.StatData == statData);
			stat.IncreaseValue(value);
		}

        public Stat GetStat(StatData statData)
        {
			return stats.Find(x => x.StatData == statData);
        }
		public void StartSession()
		{
			foreach (var stat in stats)
			{
				stat.StartSession();
			}
		}
    }
}