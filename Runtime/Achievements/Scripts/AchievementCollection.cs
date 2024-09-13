using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.Progression

{
    [CreateAssetMenu(menuName = "HexTecGames/Progression/AchievementCollection")]
    public class AchievementCollection : ScriptableObject
	{
		public List<AchievementData> achievementDatas = new List<AchievementData>();
	}
}