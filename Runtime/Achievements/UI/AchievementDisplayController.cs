using HexTecGames.Basics.UI;

namespace HexTecGames.Progression.Achievements.UI
{
    public class AchievementDisplayController : DisplayController<AchievementDisplay, Achievement>
    {
        private void Start()
        {
            SetItems(AchievementManager.Achievements);
            AchievementManager.OnReset += AchievementManager_OnReset;
        }

        private void OnDestroy()
        {
            AchievementManager.OnReset -= AchievementManager_OnReset;
        }

        private void AchievementManager_OnReset()
        {
            SetItems(AchievementManager.Achievements);
        }
    }
}