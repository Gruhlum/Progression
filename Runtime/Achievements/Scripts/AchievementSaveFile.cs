using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.Progression
{
	[System.Serializable]
	public class AchievementSaveFile
	{
		public List<AchievementSaveData> saveDatas = new List<AchievementSaveData>();

		public AchievementSaveFile(List<Achievement> achievements)
		{
			foreach (var achievement in achievements)
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