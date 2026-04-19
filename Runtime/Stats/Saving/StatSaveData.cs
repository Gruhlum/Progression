namespace HexTecGames.Progression
{
    [System.Serializable]
    public class StatSaveData
    {
        public string name;
        public double value;

        public StatSaveData(Stat stat)
        {
            name = stat.StatType.name;
            value = stat.Value;
        }
    }
}