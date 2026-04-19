using HexTecGames.Basics.UI;
using UnityEngine;

namespace HexTecGames.Progression.Achievements.UI
{
    public class AchievementDisplayController : DisplayController<AchievementDisplay, Achievement>
    {
        private void OnEnable()
        {
            SetItems(AchievementManager.Achievements);
        }
    }
}