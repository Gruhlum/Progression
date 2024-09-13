using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.Progression
{
    public class AchievmentPopupController : MonoBehaviour
    {
        [SerializeField] private AchievementManager achievementM = default;
        [SerializeField] private AchievementPopupDisplay popupDisplay = default;
        [SerializeField] private SteamManager steamManager = default;
        [Space]
        [SerializeField] private AchievementData testAchievement = default;

        private void Awake()
        {
            achievementM.OnAchievementCompleted += AchievementM_OnAchievementCompleted;
        }

        private void OnDestroy()
        {
            achievementM.OnAchievementCompleted -= AchievementM_OnAchievementCompleted;
        }

        [ContextMenu("Show Test Achievement")]
        public void ShowTestAchievement()
        {
            if (testAchievement == null)
            {
                Debug.Log("No test achievement referenced!");
                return;
            }
            DisplayAchievement(new Achievement(testAchievement, true));
        }
        private void DisplayAchievement(Achievement achievement)
        {
            if (steamManager != null)
            {
                steamManager.UnlockAchievement(achievement.Data.name);
            }
            else popupDisplay.DisplayAchievement(achievement);
        }
        private void AchievementM_OnAchievementCompleted(Achievement achievement)
        {
            DisplayAchievement(achievement);
        }
    }
}