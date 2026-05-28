using System.Collections;
using System.Collections.Generic;
using HexTecGames.Basics;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using HexTecGames.Progression;

namespace HexTecGames.AchievementExample
{
    public class TimeStatController : AdvancedBehaviour
    {
        [SerializeField] private StatType timeStatType = default;

        private Stat timeStat;
        private float currentTimer;

        private void Start()
        {
            GetStat();

        }

        private void FixedUpdate()
        {
            currentTimer += Time.deltaTime;
            while (currentTimer > 1f)
            {
                currentTimer -= 1f;
                timeStat.IncreaseValue(1);
            }
        }

        private void GetStat()
        {
            var result = StatManager.FindStat(timeStatType);
            if (result != null)
            {
                timeStat = result;
            }
            else Debug.LogError("Could not find Time Stat!");
        }
    }
}