using HexTecGames.Basics;
using System;
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

        private const string SAVE_FOLDER_NAME = "Achievements";

        public event Action<Achievement> OnAchievementCompleted;

        private bool achievementsLoaded;

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
        [ContextMenu("Clear Achievements")]
        public void ClearAchievements()
        {
            SaveSystem.SaveJSON(new AchievementSaveFile(new List<Achievement>()), SAVE_FOLDER_NAME);
        }
        public void CompleteAchievement(Achievement achievement)
        {
            if (achievement.Completed)
            {
                Debug.Log("Achievement " + achievement.Data.name + " already unlocked");
                return;
            }
            Debug.Log("Unlocking achievement " + achievement.Data.name);
            achievement.Complete();
            OnAchievementCompleted?.Invoke(achievement);
            SaveAchievements();
        }
        public void CompleteAchievement(string name)
        {
            LoadAchievements();
            var achievement = achievements.Find(x => x.Data.name == name);
            if (achievement == null)
            {
                Debug.Log("Could not find achievement with name: " + name);
                return;
            }
            else CompleteAchievement(achievement);
        }
        public void SaveAchievements()
        {
            SaveSystem.SaveJSON(new AchievementSaveFile(achievements), SAVE_FOLDER_NAME);
        }
        private void LoadAchievements()
        {
            if (achievementsLoaded)
            {
                return;
            }
            AchievementSaveFile saveFile = SaveSystem.LoadJSON<AchievementSaveFile>(SAVE_FOLDER_NAME);
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
            LoadAchievements();
            return achievements.FindAll(a => a is StatAchievement statA && statA.AchievementData.LinkedStat == data).ToArray();
        }
    }
}