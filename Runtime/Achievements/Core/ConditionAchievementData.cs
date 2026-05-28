using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.Progression
{
    [CreateAssetMenu(menuName = "HexTecGames/Progression/ConditionalAchievementData")]
    public class ConditionalAchievementData : AchievementData
    {
        [TextArea] public string description;
        public Sprite Icon;
        public Sprite incompletedIcon;

        public override Achievement CreateAchievement(AchievementSaveFile saveFile)
        {
            bool completed = false;
            if (saveFile != null)
            {
                completed = saveFile.GetAchievementStatus(name);
            }
            return new ConditionalAchievement(this, completed);
        }
    }
}