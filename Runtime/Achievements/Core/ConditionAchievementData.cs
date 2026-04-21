using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.Progression
{
    [CreateAssetMenu(menuName = "HexTecGames/Progression/ConditionAchievementData")]
    public class ConditionAchievementData : AchievementData
    {
        [TextArea] public string description;
        public Sprite icon;

        public override List<Achievement> CreateAchievements(AchievementSaveFile saveFile)
        {
            bool completed = false;
            if (saveFile != null)
            {
                completed = saveFile.GetAchievementStatus(name);
            }
            return new List<Achievement>() { new Achievement(name, description,  icon, completed) };
        }
    }
}