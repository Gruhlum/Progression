using UnityEngine;

namespace HexTecGames.Progression
{
    public class AchievementPopupController : MonoBehaviour
    {
        [SerializeField] private AchievementPopupDisplay popupDisplay = default;
        //[SerializeField] private SteamManager steamManager = default;
        [Space]
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
            DisplayAchievement(new Achievement("Test Achievement", null, true));
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