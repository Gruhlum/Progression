using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HexTecGames.Progression
{
    public class AchievementPopupDisplay : MonoBehaviour
    {
        [SerializeField] private Image icon = default;
        [SerializeField] private TMP_Text nameGUI = default;
        [Space]
        [SerializeField] private float duration = 3;
        [SerializeField] private float speedMultiplier = 2;
        [Space]
        [SerializeField] private Vector3 startPos = default;
        [SerializeField] private Vector3 endPos = default;


#if UNITY_EDITOR
        [ContextMenu("Set End Position")]
        public void SetEndPos()
        {
            endPos = transform.position;
        }
        [ContextMenu("Set Start Position")]
        public void SetStartPos()
        {
            startPos = transform.position;
        }
#endif
        public void DisplayAchievement(Achievement achievement)
        {
            icon.sprite = achievement.Data.Icon;
            nameGUI.text = achievement.Data.name;
            gameObject.SetActive(true);
            StartCoroutine(AnimateMoveIn());
        }

        private IEnumerator AnimateMoveIn()
        {
            yield return MoveToPostion(startPos, endPos);
            yield return new WaitForSeconds(duration);
            yield return MoveToPostion(endPos, startPos);
            gameObject.SetActive(false);
        }
        private IEnumerator MoveToPostion(Vector3 startPos, Vector3 endPos)
        {
            float timer = 0;
            while (timer < 1f)
            {                
                timer += Time.deltaTime * speedMultiplier;
                transform.position = Vector3.Lerp(startPos, endPos, Mathf.SmoothStep(0, 1, timer));
                yield return null;
            }
        }
    }
}