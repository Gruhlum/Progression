using HexTecGames.Basics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace HexTecGames.Progression
{
    public class AchievementManager : MonoBehaviour
    {
        //[SerializeField] private StatsManager statsManager = default;
        [SerializeField] private AchievementCollection achievementDatas = default;

        public static ReadOnlyCollection<Achievement> Achievements
        {
            get
            {
                return achievements.AsReadOnly();
            }
        }
        private static List<Achievement> achievements = new List<Achievement>();

        private const string SAVE_FOLDER_NAME = "ACHIEVEMENTS";

        public static event Action<Achievement> OnAchievementCompleted;
        public static event Action OnReset;

        private static bool achievementsLoaded;

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

        [MenuItem("Tools/SaveSystem/Reset Achievements")]
        public static void ClearAchievements()
        {
            if (Application.isPlaying)
            {
                foreach (var achievement in achievements)
                {
                    achievement.Reset();
                }
                SaveAchievements();
                OnReset?.Invoke();
            }
            else SaveSystem.DeleteFile(SAVE_FOLDER_NAME);
        }

        public List<StatAchievement> GetStatAchievements(StatType statData)
        {
            List<StatAchievement> results = new List<StatAchievement>();

            foreach (var achievement in achievements)
            {
                if (achievement is StatAchievement statAchievement && statAchievement.AchievementData.LinkedStat == statData)
                {
                    results.Add(statAchievement);
                }
            }

            return results;
        }
        public static void CompleteAchievement(Achievement achievement)
        {
            if (achievement.Completed)
            {
                Debug.Log("Achievement " + achievement.Data.name + " already unlocked");
                return;
            }
            Debug.Log("Unlocking achievement " + achievement.Data.name);
            achievement.Complete();
            SaveAchievements();
        }
        public static void CompleteAchievement(string name)
        {
            var achievement = achievements.Find(x => x.Data.name == name);
            if (achievement == null)
            {
                Debug.Log("Could not find achievement with name: " + name);
                return;
            }
            else CompleteAchievement(achievement);
        }
        public static void SaveAchievements()
        {
            SaveSystem.SaveJSON(new AchievementSaveFile(achievements), SAVE_FOLDER_NAME);
        }
        private void LoadAchievements()
        {
            if (achievementsLoaded)
            {
                return;
            }
            achievementsLoaded = true;
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
                AddAchievement(data.CreateAchievement(false));
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
                AddAchievement(data.CreateAchievement(saveFile.GetAchievementStatus(data)));
            }
        }
        private static void AddAchievement(Achievement achievement)
        {
            achievements.Add(achievement);
            achievement.OnCompleted += Achievement_OnCompleted;
        }

        private static void Achievement_OnCompleted(Achievement achievement)
        {
            OnAchievementCompleted?.Invoke(achievement);
            SaveAchievements();
        }
    }
}