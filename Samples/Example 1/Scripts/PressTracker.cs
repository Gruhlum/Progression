using System.Collections;
using System.Collections.Generic;
using HexTecGames.Basics;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace HexTecGames.Progression.AchievementExample
{
    public class PressTracker : AdvancedBehaviour
    {
        [SerializeField] private ConditionalAchievementData data = default;
        [SerializeField] private KeyCode keyCode = KeyCode.E;

        private Achievement achievement;


        private void Start()
        {
            achievement = AchievementManager.FindAchievement(data);
            if (achievement.Completed)
            {
                gameObject.SetActive(false);
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(keyCode))
            {
                var achievement = AchievementManager.FindAchievement(data);
                achievement.Complete();
                Debug.Log($"Unlocked achievement: {achievement} with {keyCode}");
                gameObject.SetActive(false);
            }
        }
    }
}