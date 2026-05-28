using System.Collections;
using System.Collections.Generic;
using HexTecGames.Progression.Stats;
using UnityEngine;

namespace HexTecGames.Progression
{
    [System.Serializable]
    public class VirtualAchievement : ProgressionAchievement
    {
        public int Value { get; }

        public override int CurrentValue { get; }
        public override int TargetValue { get; }

        public VirtualAchievement(StatAchievement statAchievement, int currentValue, int targetValue) 
            : base(statAchievement.Name, statAchievement.Description, statAchievement.Icon, statAchievement.IncompletedIcon, true)
        {
            CurrentValue = currentValue;
            TargetValue = targetValue;
        }
    }
}