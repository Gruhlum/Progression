using HexTecGames.Basics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.Progression
{
	[System.Serializable]
	public class Achievement
	{
        public AchievementData Data
        {
            get
            {
                return data;
            }
            private set
            {
                data = value;
            }
        }
        private AchievementData data;

        public bool Completed
        {
            get
            {
                return completed;
            }
            private set
            {
                completed = value;
            }
        }
        private bool completed;

        private const string SAVEFOLDERNAME = "Achievements";

        public Achievement(AchievementData data, bool completed)
        {
            this.Data = data;
            this.Completed = completed;
        }

        public void Complete()
        {
            Completed = true;
        }

        public static void SaveAchievements(List<Achievement> achievements)
        {
            SaveSystem.SaveJSON(new AchievementSaveFile(achievements), SAVEFOLDERNAME);
        }
        public static List<Achievement> LoadAchievements(List<AchievementData> achievementDatas)
        {
            AchievementSaveFile saveFile = SaveSystem.LoadJSON<AchievementSaveFile>(SAVEFOLDERNAME);
            if (saveFile == null)
            {
                return CreateAchievements(achievementDatas);
            }
            else return CreateAchievements(saveFile, achievementDatas);
        }
        private static List<Achievement> CreateAchievements(List<AchievementData> achievementDatas)
        {
            List<Achievement> results = new List<Achievement>();
            foreach (var data in achievementDatas)
            {
                results.Add(data.CreateAchievement(false));
            }
            return results;
        }
        private static List<Achievement> CreateAchievements(AchievementSaveFile saveFile, List<AchievementData> achievementDatas)
        {
            if (saveFile == null)
            {
                return CreateAchievements(achievementDatas);
            }

            List<Achievement> results = new List<Achievement>();

            foreach (var data in achievementDatas)
            {
                results.Add(data.CreateAchievement(saveFile.GetAchievementStatus(data)));
            }
            return results;
        }
        public static Achievement[] GetStatsAchievements(StatData data, List<Achievement> achievements)
        {
            return achievements.FindAll(a => a is StatAchievement statA && statA.AchievementData.LinkedStat == data).ToArray();
        }
    }
}