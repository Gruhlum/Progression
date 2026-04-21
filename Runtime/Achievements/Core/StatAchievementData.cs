using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using HexTecGames.Basics;
using HexTecGames.Progression.Stats;
using UnityEngine;

namespace HexTecGames.Progression
{
    [CreateAssetMenu(menuName = "HexTecGames/Progression/StatAchievementData")]
    public class StatAchievementData : AchievementData
    {
        public string Description
        {
            get
            {
                return description;
            }
            private set
            {
                description = value;
            }
        }
        [SerializeField, TextArea] private string description;

        [SerializeField, ReadOnly] private string singularDescription = default;
        [SerializeField, ReadOnly] private string pluralDescription = default;

        public StatType LinkedStat
        {
            get
            {
                return linkedStatType;
            }
            private set
            {
                linkedStatType = value;
            }
        }

        [Space][SerializeField] private StatType linkedStatType;

        public List<StepData> stepDatas = new List<StepData>();



        private void OnValidate()
        {
            singularDescription = FormatWithPlural(Description, 1);
            pluralDescription = FormatWithPlural(Description, 10);
        }

        public static string FormatWithPlural(string template, int value)
        {
            // Replace the number placeholder
            string result = template.Replace("#", value.ToString());

            // New pluralization pattern: {singular|plural}
            return Regex.Replace(result, @"\{([^|]+)\|([^}]+)\}", match =>
            {
                string singular = match.Groups[1].Value;
                string plural = match.Groups[2].Value;
                return value == 1 ? singular : plural;
            });
        }

        public override List<Achievement> CreateAchievements(AchievementSaveFile saveFile)
        {
            var results = new List<Achievement>();
            var stat = StatManager.FindStat(linkedStatType);
            if (stat == null)
            {
                Debug.LogError($"Could not find stat {linkedStatType}");
                return results;
            }
            int count = 0;
            foreach (var data in stepDatas)
            {
                string achievementName = $"{name}_{count}";
                count++;
                string actualDescription = FormatWithPlural(Description, data.targetValue);
                bool completed = false;
                if (saveFile != null)
                {
                    completed = saveFile.GetAchievementStatus(achievementName);
                }
                results.Add(new StatAchievement(linkedStatType, achievementName, actualDescription, data.icon, completed));
            }
            return results;
        }
    }
}