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
        public bool GetAchievementStatus(string id)
        {
            AchievementSaveData saveData = saveDatas.Find(x => x.name == id);
            if (saveData == null)
            {
                return false;
            }
            return saveData.completed;
        }
    }
}