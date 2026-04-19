using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.Progression
{
    [CreateAssetMenu(menuName = "HexTecGames/Progression/ConditionAchievementData")]
    public class ConditionAchievementData : AchievementData
    {
        public Sprite Icon;

        public override List<Achievement> CreateAchievements(bool completed)
        {
            return new List<Achievement>() { new Achievement(name, Icon) };
        }
    }
}