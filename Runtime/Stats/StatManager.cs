using HexTecGames.Basics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEditor;
using UnityEngine;

namespace HexTecGames.Progression
{
    public class StatManager : MonoBehaviour
    {
        [SerializeField] private AchievementManager achievementsManager = default;
        [SerializeField] private List<StatType> statDatas = default;

        public static ReadOnlyCollection<Stat> Stats
        {
            get
            {
                return stats.AsReadOnly();
            }
        }
        private static List<Stat> stats = new List<Stat>();

        private const string SAVE_FOLDER_NAME = "STATS";

        void Awake()
        {
            LoadData();
        }
        void OnDestroy()
        {
            SaveData();
        }

        [MenuItem("Tools/SaveSystem/Reset Stats")]
        public static void ClearStats()
        {
            if (Application.isPlaying)
            {
                StatSaveFile saveFile = new StatSaveFile(null);
                SaveSystem.SaveJSON(saveFile, SAVE_FOLDER_NAME);
                foreach (var stat in stats)
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
            foreach (var statData in statDatas)
            {
                stats.Add(new Stat(statData, 0, achievementsManager.GetStatAchievements(statData)));
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
                stats.Add(new Stat(statData, saveFile.RetrieveValue(statData), achievementsManager.GetStatAchievements(statData)));
            }
        }

        public static Stat GetStat(StatType statData)
        {
            return stats.Find(x => x.StatData == statData);
        }
        public static void StartSession()
        {
            foreach (var stat in stats)
            {
                stat.StartSession();
            }
        }
    }
}