using System.Collections;
using System.Collections.Generic;
using HexTecGames.EaseFunctions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HexTecGames.Progression
{
    public class AchievementPopupDisplay : MonoBehaviour
    {
        [SerializeField] private RectTransform rectT = default;
        [Space]
        [SerializeField] private Image icon = default;
        [SerializeField] private TMP_Text nameGUI = default;
        [SerializeField] private TMP_Text descriptionGUI = default;
        [Header("Settings")]
        [SerializeField] private float waitTime = 3f;
        [SerializeField] private float queueSpeedMulti = 2f;
        [Header("Animation")]
        [SerializeField] private float animationSpeed = 2f;
        [SerializeField] private EaseFunction easingIn = default;
        [SerializeField] private EaseFunction easingOut = default;

        private Queue<Achievement> achievementQueue = new Queue<Achievement>();

        private Coroutine animationCoroutine;

        public void AddAchievement(Achievement achievement)
        {
            achievementQueue.Enqueue(achievement);
            if (animationCoroutine == null)
            {
                gameObject.SetActive(true);
                animationCoroutine = StartCoroutine(DisplayAchievements());
            }
        }

        private void DisplayAchievement(Achievement achievement)
        {
            icon.sprite = achievement.Icon;
            nameGUI.text = achievement.Name;
            descriptionGUI.text = achievement.Description;
        }

        private IEnumerator DisplayAchievements()
        {
            while (achievementQueue.Count > 0)
            {
                Achievement achievement = achievementQueue.Dequeue();
                yield return AnimatePopup(achievement);
            }
            animationCoroutine = null;
            gameObject.SetActive(false);
        }

        private IEnumerator AnimatePopup(Achievement achievement)
        {
            DisplayAchievement(achievement);
            float height = rectT.sizeDelta.y * rectT.lossyScale.y;

            Vector3 startPos = transform.position;
            Vector3 targetPos = startPos + new Vector3(0, height, 0);
            yield return AnimateMovement(startPos, targetPos, easingIn);
            yield return new WaitForSeconds(waitTime / (achievementQueue.Count > 0 ? queueSpeedMulti : 1f));
            yield return AnimateMovement(targetPos, startPos, easingOut);
            yield return new WaitForSeconds(0.1f);
        }

        private IEnumerator AnimateMovement(Vector2 startPos, Vector2 targetPos, EaseFunction easing)
        {
            float timer = 0f;

            while (timer < 1f)
            {
                timer += Time.deltaTime * animationSpeed * (achievementQueue.Count > 0 ? queueSpeedMulti : 1f);
                float progress = easing.GetValue(timer);
                transform.position = Vector2.Lerp(startPos, targetPos, progress);
                yield return null;
            }
        }
    }
}