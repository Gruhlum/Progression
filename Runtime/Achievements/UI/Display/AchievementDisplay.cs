using System.Collections;
using System.Collections.Generic;
using HexTecGames.Basics;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using HexTecGames.Basics.UI;
using static HexTecGames.Progression.Enums;

namespace HexTecGames.Progression.Achievements.UI.Display
{
    public abstract class AchievementDisplay<D, T> : Display<D, T> where T : Achievement where D : AchievementDisplay<D, T> 
    {
        [SerializeField] protected Image border = default;
        [SerializeField] protected Image icon = default;

        [SerializeField] protected TMP_Text descriptionGUI = default;

        [Space]
        [SerializeField] private CompletionStyle completionStyle = default;
        [Space]
        [DrawIf(nameof(completionStyle), CompletionStyle.Color), SerializeField] private Color incompletedColor = Color.black;
        [DrawIf(nameof(completionStyle), CompletionStyle.Color), SerializeField] private Color completedColor = Color.yellow;


        protected virtual void OnEnable()
        {
            if (Item != null)
            {
                SetCompletionState(Item.Completed);
            }
        }

        protected override void DrawItem(T item)
        {
            descriptionGUI.text = item.Description;
            icon.sprite = item.Icon;
            SetCompletionState(item.Completed);
        }
        protected override void AddEvents(T achievement)
        {
            base.AddEvents(achievement);
            achievement.OnCompleted += Achievement_OnCompleted;
        }

        protected override void RemoveEvents(T achievement)
        {
            base.RemoveEvents(achievement);
            achievement.OnCompleted -= Achievement_OnCompleted;
        }
        protected virtual void Achievement_OnCompleted(Achievement achievement)
        {
            SetCompletionState(true);
        }
        private void SetCompletionState(bool isComplete)
        {
            if (Item == null)
            {
                return;
            }
            if (completionStyle == CompletionStyle.Color)
            {
                border.color = isComplete ? completedColor : incompletedColor;
            }
            else if (completionStyle == CompletionStyle.Icon)
            {
                icon.sprite = isComplete ? Item.Icon : Item.IncompletedIcon;
            }
        }
    }
}