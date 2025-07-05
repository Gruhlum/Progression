using HexTecGames.Basics.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HexTecGames.Progression
{
    public class AchievementDisplay : Display<AchievementDisplay, Achievement>
    {
        [SerializeField] private Image border = default;
        [SerializeField] private Image icon = default;

        [SerializeField] private TMP_Text nameGUI = default;
        [Space]
        [SerializeField] private Color uncompletedColor = Color.black;
        [SerializeField] private Color completedColor = Color.yellow;


        protected override void DrawItem(Achievement item)
        {
            if (item == null)
            {
                return;
            }
            int difficultyIndex = (int)item.Data.Difficulty;
            nameGUI.text = item.Data.name;
            icon.sprite = item.Data.Icon;
            UpdateBorderColor();
        }

        private void UpdateBorderColor()
        {
            if (Item == null)
            {
                return;
            }
            border.color = Item.Completed ? completedColor : uncompletedColor;
        }

        protected override void AddEvents(Achievement achievement)
        {
            base.AddEvents(achievement);
            achievement.OnCompleted += Achievement_OnCompleted;
        }

        protected override void RemoveEvents(Achievement achievement)
        {
            base.RemoveEvents(achievement);
            achievement.OnCompleted -= Achievement_OnCompleted;
        }
        private void Achievement_OnCompleted(Achievement achievement)
        {
            UpdateBorderColor();
        }
    }
}