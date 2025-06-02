using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.Progression.Samples.Example1
{
    public class KillButton : MonoBehaviour
    {
        [SerializeField] private StatType killType = default;
        [SerializeField] private int killsToGain = 1;

        private Stat killStat;

        private void Start()
        {
            killStat = StatManager.GetStat(killType);
        }

        public void Clicked()
        {
            killStat.IncreaseValue(killsToGain);
        }
    }
}