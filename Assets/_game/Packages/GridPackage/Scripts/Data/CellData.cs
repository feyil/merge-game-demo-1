using System;
using UnityEngine;

namespace _game.Packages.GridPackage.Scripts.Data
{
    [Serializable]
    public class CellData
    {
        public Vector2Int Cord;
        public string ItemType;
        public string Data;
    }
}