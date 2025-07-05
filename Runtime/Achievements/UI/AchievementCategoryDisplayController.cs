using System.Collections.Generic;
using System.Linq;
using HexTecGames.Basics;
using UnityEngine;

namespace HexTecGames.Progression
{
    public class AchievementCategoryDisplayController : MonoBehaviour
    {
        private List<Achievement> achievements;

        [SerializeField] private AchievementCollection achievementCollection = default;
        [SerializeField] private Spawner<CategoryDisplayController> categorySpawner = default;

        private void Awake()
        {
            SetItems(Achievement.LoadAchievements(achievementCollection.GetItems()));
        }

        public void SetItems(IList<Achievement> achievements)
        {
            this.achievements = new List<Achievement>(achievements);
            DisplayItems();
        }

        protected void DisplayItems()
        {
            List<CategoryCollection<Achievement>> results = GetCategories(achievements);

            foreach (CategoryCollection<Achievement> result in results)
            {
                categorySpawner.Spawn().SetItems(result);
            }
        }
        private List<CategoryCollection<Achievement>> GetCategories(List<Achievement> achievements)
        {
            List<CategoryCollection<Achievement>> results = new List<CategoryCollection<Achievement>>();

            foreach (Achievement achievement in achievements)
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