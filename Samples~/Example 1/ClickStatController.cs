using System.Collections;
using System.Collections.Generic;
using HexTecGames.Basics;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using HexTecGames.Progression;

namespace HexTecGames
{
    public class ClickStatController : AdvancedBehaviour
    {
        [SerializeField] private StatType clickStatType = default;

        private Stat clickStat;

        private void Start()
        {
            GetStat();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                clickStat.IncreaseValue(1);
            }
        }

        private void GetStat()
        {
            var result = StatManager.FindStat(clickStatType);
            if (result != null)
            {
                clickStat = result;
            }
            else Debug.LogError("Could not find Time Stat!");
        }
    }
}