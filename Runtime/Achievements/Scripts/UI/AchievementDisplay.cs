using HexTecGames.Basics.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HexTecGames.Progression
{
    public class AchievementDisplay : Display<Achievement>
    {
        [SerializeField] private Image border = default;
        [SerializeField] private Image icon = default;

        [SerializeField] private TMP_Text nameGUI = default;

        [SerializeField] private List<Sprite> borderSprites = default;

        protected override void DrawItem(Achievement item)
        {
            int difficultyIndex = (int)item.Data.Difficulty;
            if (borderSprites != null && borderSprites.Count > difficultyIndex)
            {
                border.sprite = borderSprites[difficultyIndex];
            }
            nameGUI.text = item.Data.name;
            icon.sprite = item.Completed ? item.Data.Icon : item.Data.GreyscaleIcon;
        }
    }
}