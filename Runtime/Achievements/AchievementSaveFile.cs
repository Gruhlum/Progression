using System.Collections.Generic;

namespace HexTecGames.Progression
{
    [System.Serializable]
    public class AchievementSaveFile
    {
        public List<AchievementSaveData> saveDatas;

        public AchievementSaveFile(IList<Achievement> achievements)
        {
            saveDatas = new List<AchievementSaveData>();
            foreach (Achievement achievement in achievements)
            {
                saveDatas.Add(new AchievementSaveData(achievement));
            }
        }
        public bool GetAchievementStatus(AchievementData data)
        {
            AchievementSaveData saveData = saveDatas.Find(x => x.name == data.name);
            if (saveData == null)
            {
                return false;
            }
            return saveData.completed;
        }
    }
}