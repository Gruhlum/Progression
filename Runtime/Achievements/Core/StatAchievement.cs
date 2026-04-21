using UnityEngine;

namespace HexTecGames.Progression
{
    [System.Serializable]
    public class StatAchievement : Achievement
    {
        public Stat Stat { get; set; }

        public StatAchievement(StatType statType, string name, string description, Sprite icon, bool completed) 
            : base(name, description, icon, completed)
        {
            var stat = StatManager.FindStat(statType);
            if (stat != null)
            {
                this.Stat = stat;
            }
            else Debug.LogError($"Could not find Stat of type: {statType}");
        }
    }
}