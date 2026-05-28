using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HexTecGames.Basics;
using HexTecGames.Progression.Achievements.UI;
using UnityEditor;
using UnityEngine;

namespace HexTecGames.Progression
{
    public class AchievementManager : AdvancedBehaviour
    {
        //[SerializeField] private StatsManager statsManager = default;
        [SerializeField] private List<NamedGroup<AchievementData>> categoryDatas = default;

        public static ReadOnlyCollection<Achievement> Achievements
        {
            get
            {
                return achievements.AsReadOnly();
            }
        }
        private static List<Achievement> achievements = new List<Achievement>();

        private static List<ConditionalAchievement> conditionalAchievements;

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
            achievementsLoaded = false;
            OnReset = null;
            OnAchievementCompleted = null;
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
        
        public static Achievement FindAchievement(ConditionalAchievementData data)
        {
            return conditionalAchievements.Find(x => x.Data == data);
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
            Debug.Log("Loading Achievements ...");
            achievementsLoaded = true;
            var saveFile = SaveSystem.LoadJSON<AchievementSaveFile>(SAVE_FOLDER_NAME);
            Debug.Log($"... Save file: {saveFile != null} ...");
            CreateAchievements(saveFile);
        }

        private void CreateAchievements(AchievementSaveFile saveFile = null)
        {
            foreach (var category in categoryDatas)
            {
                foreach (var data in category.Datas)
                {
                    AddAchievement(data.CreateAchievement(saveFile));
                }
            }
            Debug.Log($"... Added {Achievements.Count} achievements from {categoryDatas.Count} categories and {categoryDatas.Sum(x => x.Datas.Count)} AchievementDatas");
            conditionalAchievements = achievements.OfType<ConditionalAchievement>().ToList();
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