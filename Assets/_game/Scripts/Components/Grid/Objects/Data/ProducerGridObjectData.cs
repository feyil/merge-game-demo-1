using System;

namespace _game.Scripts.Components.Grid.Objects.Data
{
    [Serializable]
    public class ProducerGridObjectData
    {
        public int Capacity;

        public static ProducerGridObjectData GetDefaultData()
        {
            return new ProducerGridObjectData()
            {
                Capacity = 10
            };
        }
    }
}