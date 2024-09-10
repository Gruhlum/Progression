using HexTecGames.Basics;
using HexTecGames.Basics.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HexTecGames.Progression
{
	public class AchievementDisplayController : MonoBehaviour
	{
        private List<Achievement> achievements;

        [SerializeField] private AchievementCollection achievementCollection = default;
        [SerializeField] private Spawner<CategoryDisplayController> categorySpawner = default;

        void Awake()
        {
            SetItems(Achievement.LoadAchievements(achievementCollection.achievementDatas));
        }

        public void SetItems(List<Achievement> achievements)
        {
            this.achievements = achievements;
            DisplayItems();
        }

        protected void DisplayItems()
        {
            var results = GetCategories(achievements);

            foreach (var result in results)
            {
                categorySpawner.Spawn().SetItems(result);
            }
        }
        private List<CategoryCollection<Achievement>> GetCategories(List<Achievement> achievements)
        {
            List<CategoryCollection<Achievement>> results = new List<CategoryCollection<Achievement>>();

            foreach (var achievement in achievements)
            {
                if (results.Any(x => x.category == achievement.Data.Category))
                {
                    results.Find(x => x.category == achievement.Data.Category).items.Add(achievement);
                }
                else results.Add(new CategoryCollection<Achievement>(achievement.Data.Category, achievement));
            }
            return results;
        }
    }
}