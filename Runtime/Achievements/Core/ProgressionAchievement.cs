using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.Progression
{
    [System.Serializable]
    public abstract class ProgressionAchievement : Achievement
    {
        public abstract int CurrentValue { get; }
        public abstract int TargetValue { get; }

        public ProgressionAchievement(string name, string description, Sprite icon, Sprite incompleteIcon, bool completed)
    : base(name, description, icon, incompleteIcon, completed)
        {

        }
    }
}