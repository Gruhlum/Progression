using HexTecGames.Basics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.Progression
{
	[CreateAssetMenu(menuName = "HexTecGames/StatAchievementData")]
	public class StatAchievementData : AchievementData
	{
        public StatData LinkedStat
        {
            get
            {
                return linkedStat;
            }
            private set
            {
                linkedStat = value;
            }
        }
        [SerializeField] private StatData linkedStat;

        public double TargetValue
        {
            get
            {
                return targetValue;
            }
            private set
            {
                targetValue = value;
            }
        }
        [SerializeField] private double targetValue;

        [SerializeField, ReadOnly] private string actualDescription = default;

        void OnValidate()
        {
            actualDescription = Description.Replace("#", TargetValue.ToString());
        }

        public override Achievement CreateAchievement(bool completed)
        {
            return new StatAchievement(this, completed);
        }
    }
}