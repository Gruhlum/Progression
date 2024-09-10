using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HexTecGames.Progression
{
    [System.Serializable]
    public class StatAchievement : Achievement
    {
        public StatAchievementData AchievementData
        {
            get
            {
                return achievementData;
            }
            private set
            {
                achievementData = value;
            }
        }
        private StatAchievementData achievementData;      

        public StatAchievement(StatAchievementData data, bool completed) : base(data, completed)
        {
            AchievementData = data;            
        }
    }
}