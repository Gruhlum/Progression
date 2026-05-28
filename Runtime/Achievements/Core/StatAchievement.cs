using System;
using System.Collections.Generic;
using HexTecGames.Progression.Stats;
using UnityEngine;

namespace HexTecGames.Progression
{
    [System.Serializable]
    public class StatAchievement : ProgressionAchievement
    {
        public override int CurrentValue => Stat.Value;
        public override int TargetValue => CurrentStepData.targetValue;

        public Stat Stat { get; set; }

        private StatAchievementData data;

        public StepData CurrentStepData { get; private set; }

        public event Action<StepData> OnStepDataCompleted;
        public event Action<int> OnCurrentValueChanged;

        public StatAchievement(StatAchievementData data, bool completed)
            : base(data.name, data.Description, data.stepDatas[0].icon, data.stepDatas[0].incompleteIcon, completed)
        {
            this.data = data;
            var stat = StatManager.FindStat(data.LinkedStat);
            if (stat == null)
            {
                Debug.LogError($"Could not find Stat of type: {data.LinkedStat}");
                return;
            }

            this.Stat = stat;
            this.Stat.OnValueChanged += Stat_OnValueChanged;
            CurrentStepData = FindActiveStepData(Stat.Value);
        }

        public List<StepData> GetCompletedStepDatas()
        {
            List<StepData> completedStepDatas = new List<StepData>();
            foreach (var stepData in data.stepDatas)
            {
                if (stepData.targetValue <= Stat.Value)
                {
                    completedStepDatas.Add(stepData);
                }
            }
            return completedStepDatas;
        }

        private StepData FindActiveStepData(int currentValue)
        {
            for (int i = 0; i < data.stepDatas.Count; i++)
            {
                if (data.stepDatas[i].targetValue > currentValue)
                {
                    return data.stepDatas[i];
                }
            }
            return null;
        }

        private void Stat_OnValueChanged(int value)
        {
            if (value >= CurrentStepData.targetValue)
            {              
                int index = data.stepDatas.IndexOf(CurrentStepData);              
                if (data.stepDatas.Count > index + 1)
                {
                    OnStepDataCompleted?.Invoke(CurrentStepData);
                    Complete(false);
                    CurrentStepData = data.stepDatas[index + 1];
                }
                else Complete(true);
            }
            OnCurrentValueChanged?.Invoke(value);
        }
    }
}