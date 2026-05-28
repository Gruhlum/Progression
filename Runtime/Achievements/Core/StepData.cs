using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.Progression
{
    [System.Serializable]
    public class StepData
    {
        public int targetValue;
        public float valueDivider = 1f;
        public string unitName;
        [Space]
        public Sprite icon;
        public Sprite incompleteIcon;
    }
}