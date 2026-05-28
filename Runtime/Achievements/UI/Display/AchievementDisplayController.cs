using System.Collections;
using System.Collections.Generic;
using HexTecGames.Basics;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using HexTecGames.Progression.Achievements.UI.Display;
using HexTecGames.Progression;
using HexTecGames.Progression.Stats;

namespace HexTecGames
{
    public class AchievementDisplayController : AdvancedBehaviour
    {
        [SerializeField] private ConditionalAchievementDisplay conditionalPrefab = default;
        [SerializeField] private ProgressionAchievementDisplay statPrefab = default;
        [Space]
        [SerializeField] private Transform parent = default;

        private Spawner<ConditionalAchievementDisplay> conditionalAchievementDisplaySpawner = default;
        private Spawner<ProgressionAchievementDisplay> statAchievementDisplaySpawner = default;


        private void Awake()
        {
            conditionalAchievementDisplaySpawner = new Spawner<ConditionalAchievementDisplay>(conditionalPrefab, parent);
            statAchievementDisplaySpawner = new Spawner<ProgressionAchievementDisplay>(statPrefab, parent);
        }

        private void OnEnable()
        {
            conditionalAchievementDisplaySpawner.DeactivateAll();
            statAchievementDisplaySpawner.DeactivateAll();
            DisplayAchievements(AchievementManager.Achievements);
        }

        public void DisplayAchievements(IEnumerable<Achievement> achievements)
        {
            foreach (var achievement in achievements)
            {
                if (achievement is ConditionalAchievement conditionalAchievement)
                {
                    conditionalAchievementDisplaySpawner.Spawn(false).SetItem(conditionalAchievement);
                }
                if (achievement is StatAchievement statAchievement)
                {
                    foreach (var completedStepData in statAchievement.GetCompletedStepDatas())
                    {
                        SpawnVirtualAchievement(statAchievement, completedStepData.targetValue, completedStepData.targetValue);
                    }
                    if (!statAchievement.Completed)
                    {
                        statAchievementDisplaySpawner.Spawn(false).SetItem(statAchievement);
                    }
                }
            }
        }

        private void SpawnVirtualAchievement(StatAchievement statAchievement, int currentValue, int targetValue)
        {
            VirtualAchievement virtualAchievement = new VirtualAchievement(statAchievement, currentValue, targetValue);
            statAchievementDisplaySpawner.Spawn(false).SetItem(virtualAchievement);
        }
    }
}