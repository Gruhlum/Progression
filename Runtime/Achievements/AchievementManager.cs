using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using HexTecGames.Basics;
using HexTecGames.Progression.Achievements.UI;
using UnityEditor;
using UnityEngine;

namespace HexTecGames.Progression
{
    public class AchievementManager : AdvancedBehaviour
    {
        //[SerializeField] private StatsManager statsManager = default;
        [SerializeField] private AchievementDisplayController displayController = default;
        [SerializeField] private List<AchievementGroup> categoryDatas = default;

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


        private void Awake()
        {
            LoadAchievements();
        }
        private void OnDestroy()
        {
            SaveAchievements();
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void ResetStatics()
        {
            achievements.Clear();
        }

#if UNITY_EDITOR
        [MenuItem("Tools/SaveSystem/Reset Achievements")]
#endif
        public static void ClearAchievements()
        {
            if (Application.isPlaying)
            {
                foreach (Achievement achievement in achievements)
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

            foreach (Achievement achievement in achievements)
            {
                //if (achievement is StatAchievement statAchievement && statAchievement.AchievementData.LinkedStat == statData)
                //{
                //    results.Add(statAchievement);
                //}
            }

            return results;
        }
        public static void CompleteAchievement(Achievement achievement)
        {
            if (achievement.Completed)
            {
                //Debug.Log("Achievement " + achievement.Data.name + " already unlocked");
                return;
            }
           // Debug.Log("Unlocking achievement " + achievement.Data.name);
            achievement.Complete();
            SaveAchievements();
        }
        public static void CompleteAchievement(string name)
        {
            //Achievement achievement = achievements.Find(x => x.Data.name == name);
            //if (achievement == null)
            //{
            //    Debug.Log("Could not find achievement with name: " + name);
            //    return;
            //}
            //else CompleteAchievement(achievement);
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
            foreach (var category in categoryDatas)
            {
                foreach (var data in category.Datas)
                {
                    AddAchievements(data.CreateAchievements(false));
                }
            }
        }
        private void CreateAchievements(AchievementSaveFile saveFile)
        {
            if (saveFile == null)
            {
                CreateAchievements();
                return;
            }
            foreach (var category in categoryDatas)
            {
                foreach (var data in category.Datas)
                {
                    AddAchievements(data.CreateAchievements(saveFile.GetAchievementStatus(data)));
                }
            }
        }
        private static void AddAchievements(List<Achievement> achievements)
        {
            foreach (var achievement in achievements)
            {
                AddAchievement(achievement);
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