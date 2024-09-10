using HexTecGames.Basics;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HexTecGames.Progression
{
	public class AchievementManager : MonoBehaviour
    {
        //[SerializeField] private StatsManager statsManager = default;
        [SerializeField] private List<AchievementData> achievementDatas = default;

        private List<Achievement> achievements = new List<Achievement>();

        private const string saveFolderName = "Achievements";


        private void Reset()
        {
            //statsManager = FindObjectOfType<StatsManager>();
        }
        private void Awake()
        {
            LoadAchievements();
        }
        private void OnDestroy()
        {
            SaveAchievements();
        }
        public void SaveAchievements()
        {
            SaveSystem.SaveJSON(new AchievementSaveFile(achievements), saveFolderName);
        }
        private void LoadAchievements()
        {
            AchievementSaveFile saveFile = SaveSystem.LoadJSON<AchievementSaveFile>(saveFolderName);
            if (saveFile == null)
            {
                CreateAchievements();
            }
            else CreateAchievements(saveFile);
        }
        private void CreateAchievements()
        {
            foreach (var data in achievementDatas)
            {
                achievements.Add(data.CreateAchievement(false));
            }
        }
        private void CreateAchievements(AchievementSaveFile saveFile)
        {
            if (saveFile == null)
            {
                CreateAchievements();
                return;
            }
            foreach (var data in achievementDatas)
            {
                achievements.Add(data.CreateAchievement(saveFile.GetAchievementStatus(data)));
            }
        }
        public Achievement[] GetStatsAchievements(StatData data)
        {
            return achievements.FindAll(a => a is StatAchievement statA && statA.AchievementData.LinkedStat == data).ToArray();
        }
    }
}