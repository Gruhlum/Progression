using HexTecGames.Basics.UI;
using TMPro;
using UnityEngine;

namespace HexTecGames.Progression
{
    public class StatDisplay : Display<StatDisplay, Stat>
    {
        [SerializeField] private TMP_Text nameGUI = default;
        [SerializeField] private TMP_Text valueGUI = default;
        [Space]
        [SerializeField] private bool showSession = default;

        protected override void DrawItem(Stat stat)
        {
            nameGUI.text = stat.StatType.name;
            UpdateValueText(stat);
        }

        public override void SetItem(Stat item, bool activate = true)
        {
            if (Item != null)
            {
                Item.OnValueChanged -= Stat_OnValueChanged;
            }

            base.SetItem(item, activate);

            if (Item != null)
            {
                Item.OnValueChanged += Stat_OnValueChanged;
            }
        }

        private void Stat_OnValueChanged(double value)
        {
            UpdateValueText(Item);
        }

        private void UpdateValueText(Stat stat)
        {
            if (showSession)
            {
                valueGUI.text = $"{stat.Value} ({stat.SessionValue})";
            }
            else valueGUI.text = $"{stat.Value}";
        }
    }
}