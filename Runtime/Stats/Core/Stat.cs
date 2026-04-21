using System;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.Progression
{
    [Serializable]
    public class Stat
    {
        public StatType StatType
        {
            get
            {
                return statType;
            }
            private set
            {
                statType = value;
            }
        }
        [SerializeField] private StatType statType;

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

        public event Action<double> OnValueChanged;


        public Stat(StatType statData, double value)
        {
            this.StatType = statData;
            this.Value = value;
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
        }
    }
}