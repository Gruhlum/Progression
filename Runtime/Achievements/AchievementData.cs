using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.Progression
{
    public abstract class AchievementData : ScriptableObject
    {
        public abstract List<Achievement> CreateAchievements(bool completed);
    }
}