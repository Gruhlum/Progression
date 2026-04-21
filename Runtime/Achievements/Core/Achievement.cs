using System;
using System.Collections.Generic;
using HexTecGames.Basics;
using UnityEngine;

namespace HexTecGames.Progression
{
    [System.Serializable]
    public class Achievement
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Sprite Icon { get; private set; }

        public bool Completed
        {
            get
            {
                return completed;
            }
            private set
            {
                completed = value;
            }
        }
        private bool completed;

        private const string SAVEFOLDERNAME = "Achievements";

        public event Action<Achievement> OnCompleted;


        public Achievement(string name, string description, Sprite icon)
        {
            this.Name = name;
            this.Description = description;
            this.Icon = icon;
        }
        public Achievement(string name, string description, Sprite icon, bool completed) : this(name, description, icon)
        {
            this.Completed = completed;
        }

        public void Complete()
        {
            Completed = true;
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