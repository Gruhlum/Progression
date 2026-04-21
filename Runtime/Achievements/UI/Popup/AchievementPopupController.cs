using HexTecGames.Basics;
using UnityEngine;

namespace HexTecGames.Progression
{
    public class AchievementPopupController : AdvancedBehaviour
    {
        [SerializeField] private AchievementPopupDisplay popupDisplay = default;
        //[SerializeField] private SteamManager steamManager = default;
        [Header("Debug")]
        [SerializeField] private AchievementData testAchievement = default;

        private void Awake()
        {
            AchievementManager.OnAchievementCompleted += OnAchievementCompleted;
        }

        private void OnDestroy()
        {
            AchievementManager.OnAchievementCompleted -= OnAchievementCompleted;
        }

        [ContextMenu("Show Test Achievement")]
        public void ShowTestAchievement()
        {
            if (testAchievement == null)
            {
                Debug.Log("No test achievement referenced!");
                return;
            }
            for (int i = 0; i < 3; i++)
            {
                DisplayAchievement(new Achievement("Test", "Test Description", null, true));
            }
        }
        private void DisplayAchievement(Achievement achievement)
        {
            //if (steamManager != null)
            //{
            //    steamManager.UnlockAchievement(achievement.Data.name);
            //}
            popupDisplay.AddAchievement(achievement);
        }
        private void OnAchievementCompleted(Achievement achievement)
        {
            DisplayAchievement(achievement);
        }
    }
}