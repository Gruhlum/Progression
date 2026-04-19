using System;
using System.Collections.Generic;
using HexTecGames.Basics;
using UnityEngine;

namespace HexTecGames.Progression
{
    [System.Serializable]
    public class Achievement
    {
        public string Name { get; private set; }
        public Sprite Icon { get; private set; }

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

        public event Action<Achievement> OnCompleted;


        public Achievement(string name, Sprite icon)
        {
            this.Name = name;
            this.Icon = icon;
        }
        public Achievement(string name, Sprite icon, bool completed) : this(name, icon)
        {
            this.Completed = completed;
        }

        public void Complete()
        {
            Completed = true;
            OnCompleted?.Invoke(this);
            //Debug.Log($"{Data.name} Completed!");
        }

        public static void SaveAchievements(IList<Achievement> achievements)
        {
            SaveSystem.SaveJSON(new AchievementSaveFile(achievements), SAVEFOLDERNAME);
        }
        //public static List<Achievement> LoadAchievements(IEnumerable<AchievementData> achievementDatas)
        //{
        //    AchievementSaveFile saveFile = SaveSystem.LoadJSON<AchievementSaveFile>(SAVEFOLDERNAME);
        //    if (saveFile == null)
        //    {
        //        return CreateAchievements(achievementDatas);
        //    }
        //    else return CreateAchievements(saveFile, achievementDatas);
        //}
        //private static List<Achievement> CreateAchievements(IEnumerable<AchievementData> achievementDatas)
        //{
        //    List<Achievement> results = new List<Achievement>();
        //    foreach (AchievementData data in achievementDatas)
        //    {
        //        results.Add(data.CreateAchievement(false));
        //    }
        //    return results;
        //}
        //private static List<Achievement> CreateAchievements(AchievementSaveFile saveFile, IEnumerable<AchievementData> achievementDatas)
        //{
        //    if (saveFile == null)
        //    {
        //        return CreateAchievements(achievementDatas);
        //    }

        //    List<Achievement> results = new List<Achievement>();

        //    foreach (AchievementData data in achievementDatas)
        //    {
        //        results.Add(data.CreateAchievement(saveFile.GetAchievementStatus(data)));
        //    }
        //    return results;
        //}

        public void Reset()
        {
            Completed = false;
        }
    }
}