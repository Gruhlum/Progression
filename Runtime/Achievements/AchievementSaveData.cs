namespace HexTecGames.Progression
{
    [System.Serializable]
    public class AchievementSaveData
    {
        public string name;
        public bool completed;

        public AchievementSaveData(Achievement achievement)
        {
            //name = achievement.Data.name;
            completed = achievement.Completed;
        }
    }
}