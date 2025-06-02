using System.Collections;
using System.Collections.Generic;
using HexTecGames.Basics.UI;
using UnityEngine;

namespace HexTecGames.Progression.Stats.UI
{
    public class StatDisplayController : DisplayController<StatDisplay, Stat>
    {
        private void Start()
        {
            SetItems(StatManager.Stats);
        }
    }
}