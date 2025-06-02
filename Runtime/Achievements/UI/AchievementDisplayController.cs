using System.Collections;
using System.Collections.Generic;
using HexTecGames.Basics.UI;
using UnityEngine;

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