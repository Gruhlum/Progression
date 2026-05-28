using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.Progression
{
    public abstract class AchievementData : ScriptableObject
    {
        public abstract Achievement CreateAchievement(AchievementSaveFile saveFile);
    }
}