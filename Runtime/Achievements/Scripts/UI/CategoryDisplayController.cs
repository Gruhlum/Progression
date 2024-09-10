using HexTecGames.Basics.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace HexTecGames.Progression
{
	public class CategoryDisplayController : DisplayController<Achievement>
	{
		[SerializeField] private TMP_Text nameGUI = default;
		[SerializeField] private bool showName = default;
		public void SetItems(CategoryCollection<Achievement> collection)
        {
            SetNameText(collection);
            base.SetItems(collection.items);
        }

        private void SetNameText(CategoryCollection<Achievement> collection)
        {
            if (nameGUI == null)
            {
                return;
            }
            if (showName)
            {
                nameGUI.text = collection.category.name;
            }
            else nameGUI.text = null;
        }
    }
}