using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HexTecGames.Progression
{
    [System.Serializable]
    public class Stat
    {
        public StatType StatData
        {
            get
            {
                return statData;
            }
            private set
            {
                statData = value;
            }
        }
        [SerializeField] private StatType statData;

        public double Value
        {
            get
            {
                return value;
            }
            private set
            {
                if (this.value == value)
                {
                    return;
                }
                this.value = value;
                CheckAchievement();
                OnValueChanged?.Invoke(Value);
            }
        }
        private double value;

        public double SessionValue
        {
            get
            {
                return sessionValue;
            }
            private set
            {
                sessionValue = value;
            }
        }
        private double sessionValue;


        private IList<StatAchievement> achievements;
        private StatAchievement nextAchievement;

        public event Action<double> OnValueChanged;


        public Stat(StatType statData, double value, IList<StatAchievement> achievements)
        {
            this.StatData = statData;
            this.Value = value;

            if (achievements != null && achievements.Count > 0)
            {
                //achievements = achievements.OrderBy(x => x.AchievementData.TargetValue).ToArray();
                this.achievements = achievements;
                AssignNextAchievement();
            }
        }

        public void IncreaseValue(double value)
        {
            if (value < 0)
            {
                Debug.Log("Negative Value!");
                return;
            }
            SessionValue += value;
            Value += value;
        }
        //public void SetValue(double value)
        //{
        //    if (this.Value >= value)
        //    {
        //        return;
        //    }
        //    Value = value;
        //    OnValueChanged?.Invoke(Value);
        //}

        public void StartSession()
        {
            SessionValue = 0;
            OnValueChanged?.Invoke(Value);
        }
        public void Reset()
        {
            Value = 0;
            StartSession();
            AssignNextAchievement();
        }
        private void CheckAchievement()
        {
            CheckAchievement(nextAchievement);
        }
        private void CheckAchievement(StatAchievement achievement)
        {
            if (achievement == null)
            {
                return;
            }
            if (achievement.Completed)
            {
                Debug.Log("Achievement already completed");
                //RemoveLastAchievement();
                CheckAchievement();
                return;
            }
            if (achievement.AchievementData.TargetValue <= Value)
            {
                CompleteAchievement(achievement);
            }
        }

        private void CompleteAchievement(StatAchievement achievement)
        {
            achievement.Complete();
            RemoveLastAchievement();
        }

        private void RemoveLastAchievement()
        {
            achievements.Remove(nextAchievement);
            nextAchievement = null;
            AssignNextAchievement();
        }

        private void AssignNextAchievement()
        {
            for (int i = 0; i < achievements.Count; i++)
            {
                if (!achievements[i].Completed)
                {
                    nextAchievement = achievements[i];
                    CheckAchievement();
                    break;
                }
            }
        }
    }
}