using HexTecGames.Basics;
using UnityEngine;

namespace HexTecGames.Progression
{
    [CreateAssetMenu(menuName = "HexTecGames/Progression/StatAchievementData")]
    public class StatAchievementData : AchievementData
    {
        public StatType LinkedStat
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
        [SerializeField] private StatType linkedStat;

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

        private void OnValidate()
        {
            actualDescription = Description.Replace("#", TargetValue.ToString());
        }

        public override Achievement CreateAchievement(bool completed)
        {
            return new StatAchievement(this, completed);
        }
    }
}