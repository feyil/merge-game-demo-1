using System;
using UnityEngine;

namespace _game.Scripts.Components.Grid.Data
{
    [Serializable]
    public class CellData
    {
        public Vector2Int Cord;
        public string ItemType;
        public string Data;
    }
}