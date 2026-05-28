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

        public int Value
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
        private int value;

        public int SessionValue
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
        private int sessionValue;

        public event Action<int> OnValueChanged;


        public Stat(StatType statData, int value)
        {
            this.StatType = statData;
            this.Value = value;
        }

        public void IncreaseValue(int value)
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

        public override string ToString()
        {
            return $"{StatType} {Value}";
        }
    }
}