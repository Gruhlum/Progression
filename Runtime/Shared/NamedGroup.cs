using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.Progression
{
    [System.Serializable]
    public class NamedGroup<T>
    {
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
        [SerializeField] private string name = default;

        public List<T> Datas;
    }
}