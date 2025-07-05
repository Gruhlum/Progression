using System.Collections.Generic;
using System.Collections.ObjectModel;
using HexTecGames.Basics;
using HexTecGames.Progression.Stats;
using UnityEditor;
using UnityEngine;

namespace HexTecGames.Progression
{
    public class StatManager : MonoBehaviour
    {
        [SerializeField] private AchievementManager achievementManager = default;
        [SerializeField] private StatCollection statDatas = default;

        public static ReadOnlyCollection<Stat> Stats
        {
            get
            {
                return stats.AsReadOnly();
            }
        }
        private static List<Stat> stats = new List<Stat>();

        private const string SAVE_FOLDER_NAME = "STATS";


        private void Reset()
        {
            achievementManager = FindObjectOfType<AchievementManager>();
        }
        private void Awake()
        {
            LoadData();
        }
        private void OnDestroy()
        {
            SaveData();
        }
#if UNITY_EDITOR
        [MenuItem("Tools/SaveSystem/Reset Stats")]
#endif
        public static void ClearStats()
        {
            if (Application.isPlaying)
            {
                StatSaveFile saveFile = new StatSaveFile(null);
                SaveSystem.SaveJSON(saveFile, SAVE_FOLDER_NAME);
                foreach (Stat stat in stats)
                {
                    stat.Reset();
                }
            }
            else SaveSystem.DeleteFile(SAVE_FOLDER_NAME);
        }
        private void LoadData()
        {
            StatSaveFile saveFile = SaveSystem.LoadJSON<StatSaveFile>(SAVE_FOLDER_NAME);
            if (saveFile == null)
            {
                GenerateStats();
            }
            else GenerateStats(saveFile);
        }
        public void SaveData()
        {
            StatSaveFile saveFile = new StatSaveFile(stats);
            SaveSystem.SaveJSON(saveFile, SAVE_FOLDER_NAME);
        }
        private void GenerateStats()
        {
            foreach (StatType statData in statDatas)
            {
                stats.Add(new Stat(statData, 0, achievementManager.GetStatAchievements(statData)));
            }
        }
        private void GenerateStats(StatSaveFile saveFile)
        {
            if (saveFile == null)
            {
                GenerateStats();
                return;
            }
            foreach (StatType statData in statDatas)
            {
                stats.Add(new Stat(statData, saveFile.RetrieveValue(statData), achievementManager.GetStatAchievements(statData)));
            }
        }

        public static Stat GetStat(StatType statData)
        {
            return stats.Find(x => x.StatData == statData);
        }
        public static void StartSession()
        {
            foreach (Stat stat in stats)
            {
                stat.StartSession();
            }
        }
    }
}