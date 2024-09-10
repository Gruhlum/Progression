using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.Progression
{
	[System.Serializable]
	public class CategoryCollection<T>
	{
		public Category category;
		public List<T> items;

        public CategoryCollection(Category category)
        {
            this.category = category;
            items = new List<T>();
        }
        public CategoryCollection(Category category, T item) : this(category)
        {
            items.Add(item);
        }
    }
}