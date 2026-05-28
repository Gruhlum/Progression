using System;
using System.Collections.Generic;
using HexTecGames.Basics;
using HexTecGames.Progression.Achievements.UI.Display;
using UnityEngine;

namespace HexTecGames.Progression
{
    [System.Serializable]
    public class Achievement
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Sprite Icon { get; private set; }
        public Sprite IncompletedIcon { get; private set; }

        public bool Completed
        {
            get
            {
                return completed;
            }
            protected set
            {
                completed = value;
            }
        }

        private bool completed;

        private const string SAVEFOLDERNAME = "Achievements";

        public event Action<Achievement> OnCompleted;




        public Achievement(string name, string description, Sprite icon, Sprite incompletedIcon)
        {
            this.Name = name;
            this.Description = description;
            this.Icon = icon;
            this.IncompletedIcon = incompletedIcon;
        }
        public Achievement(string name, string description, Sprite icon, Sprite incompletedIcon, bool completed)
            : this(name, description, icon, incompletedIcon)
        {
            this.Completed = completed;
        }

        public void Complete(bool setCompleted = true)
        {
            if (Completed)
            {
                return;
            }
            if (setCompleted)
            {
                Completed = true;
            }
            OnCompleted?.Invoke(this);
            Debug.Log($"{Name} Completed!");
        }

        public static void SaveAchievements(IList<Achievement> achievements)
        {
            SaveSystem.SaveJSON(new AchievementSaveFile(achievements), SAVEFOLDERNAME);
        }

        public void Reset()
        {
            Completed = false;
        }
    }
}