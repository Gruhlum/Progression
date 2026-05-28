using System.Collections;
using System.Collections.Generic;
using HexTecGames.Basics.UI;
using UnityEngine;

namespace HexTecGames.Progression.Achievements.UI.Display
{
    [System.Serializable]
    public class ProgressionAchievementDisplay : AchievementDisplay<ProgressionAchievementDisplay, ProgressionAchievement> 
    {
        [SerializeField] protected CountSlider countSlider = default;

        protected override void DrawItem(ProgressionAchievement item)
        {
            if (item == null)
            {
                return;
            }
            base.DrawItem(item);
            countSlider.Setup(item.CurrentValue, item.TargetValue);
        }
    }
}