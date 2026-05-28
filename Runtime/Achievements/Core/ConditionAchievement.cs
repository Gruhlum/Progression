using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.Progression
{
    [System.Serializable]
    public class ConditionalAchievement : Achievement
    {
        public ConditionalAchievementData Data { get; set; }

        public ConditionalAchievement(ConditionalAchievementData data, bool completed) 
            : base(data.name, data.description, data.Icon, data.incompletedIcon, completed)
        {
            this.Data = data;
        }
    }
}