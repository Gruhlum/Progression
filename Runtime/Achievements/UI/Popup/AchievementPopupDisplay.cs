using System.Collections.Generic;
using HexTecGames.TweenLib;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HexTecGames.Progression
{
    public class AchievementPopupDisplay : MonoBehaviour
    {
        [SerializeField] private Image icon = default;
        [SerializeField] private TMP_Text nameGUI = default;
        [SerializeField] private TweenPlayer tweenPlayer = default;

        private Queue<Achievement> achievementQueue = new Queue<Achievement>();


        private void Awake()
        {
            gameObject.SetActive(false);
        }
        private void OnEnable()
        {
            tweenPlayer.OnDisabled += TweenPlayer_OnDisabled;
        }
        private void OnDisable()
        {
            tweenPlayer.OnDisabled -= TweenPlayer_OnDisabled;
        }
        private void TweenPlayer_OnDisabled(TweenPlayerBase tweenPlayer)
        {
            if (achievementQueue.Count > 0)
            {
                Achievement next = achievementQueue.Dequeue();
                DisplayAchievement(next);
            }
            else gameObject.SetActive(false);
        }

        public void AddAchievement(Achievement achievement)
        {
            if (gameObject.activeSelf)
            {
                achievementQueue.Enqueue(achievement);
            }
            else DisplayAchievement(achievement);
        }

        private void DisplayAchievement(Achievement achievement)
        {
            icon.sprite = achievement.Data.Icon;
            nameGUI.text = achievement.Data.name;
            gameObject.SetActive(true);
            tweenPlayer.Play();
        }
    }
}