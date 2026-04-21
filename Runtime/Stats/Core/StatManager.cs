using System.Collections.Generic;
using System.Collections.ObjectModel;
using HexTecGames.Basics;
using HexTecGames.Progression.Stats;
using UnityEditor;
using UnityEngine;

namespace HexTecGames.Progression
{
    public class StatManager : AdvancedBehaviour
    {
        [SerializeField] private AchievementManager achievementManager = default;
        [SerializeField] private List<NamedGroup<StatType>> statGroups = default;

        public static ReadOnlyCollection<Stat> Stats
        {
            get
            {
                return stats.AsReadOnly();
            }
        }
        private static List<Stat> stats = new List<Stat>();

        private const string SAVE_FOLDER_NAME = "STATS";

        [Header("Settings")]
        [SerializeField] private bool autoSave = default;
        [SerializeField] private bool autoLoad = default;


        private void Awake()
        {
            if (autoLoad)
            {
                LoadData();
            }
            else GenerateStats();
        }
        private void OnDestroy()
        {
            if (autoSave)
            {
                SaveData();
            }
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void ResetStatics()
        {
            stats.Clear();
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
            foreach (var statData in statGroups)
            {
                foreach (var data in statData.Datas)
                {
                    stats.Add(new Stat(data, 0));
                }
            }
        }
        private void GenerateStats(StatSaveFile saveFile)
        {
            if (saveFile == null)
            {
                GenerateStats();
                return;
            }
            foreach (var statData in statGroups)
            {
                foreach (var data in statData.Datas)
                {
                    stats.Add(new Stat(data, saveFile.RetrieveValue(data)));
                }
            }
        }

        public static Stat FindStat(StatType statData)
        {
            return stats.Find(x => x.StatType == statData);
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