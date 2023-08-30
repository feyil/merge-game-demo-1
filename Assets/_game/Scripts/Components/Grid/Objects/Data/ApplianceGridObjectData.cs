using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _game.Scripts.Components.Grid.Objects.Data
{
    [Serializable]
    public class ApplianceGridObjectData
    {
        public static int MIN_VALUE = 2;
        public static int MAX_VALUE = 2048;

        public int Number;

        public static ApplianceGridObjectData GetRandomData()
        {
            return new ApplianceGridObjectData()
            {
                Number = (int)Mathf.Pow(MIN_VALUE, Random.Range(1, (int)Mathf.Log(MAX_VALUE, 2) + 1))
            };
        }
    }
}