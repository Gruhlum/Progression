using HexTecGames.Basics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.Progression
{
    public abstract class AchievementData : ScriptableObject
    {
        public string Description
        {
            get
            {
                return description;
            }
            private set
            {
                description = value;
            }
        }
        [SerializeField, TextArea] private string description;
        public Difficulty Difficulty
        {
            get
            {
                return difficulty;
            }
            private set
            {
                difficulty = value;
            }
        }
        [SerializeField] private Difficulty difficulty;

        public Sprite Icon
        {
            get
            {
                return icon;
            }
            private set
            {
                icon = value;
            }
        }
        [SerializeField] private Sprite icon;

        public Sprite GreyscaleIcon
        {
            get
            {
                return grayscaleIcon;
            }
            private set
            {
                grayscaleIcon = value;
            }
        }
        [SerializeField] private Sprite grayscaleIcon;


        public Category Category
        {
            get
            {
                return category;
            }
            private set
            {
                category = value;
            }
        }
        [SerializeField] private Category category;


        public virtual Achievement CreateAchievement(bool completed)
        {
            return new Achievement(this, completed);
        }
    }
}