using System;

namespace _game.Scripts.Components.Grid.Objects.Data
{
    [Serializable]
    public class ProducerGridObjectData
    {
        public static int MAX_CAPACITY = 10;
        public static int CAPACITY_INCREASE_DURATION = 5;
        
        public int Capacity;
        public int CapacityIncreaseDuration;

        public static ProducerGridObjectData GetDefaultData()
        {
            return new ProducerGridObjectData()
            {
                Capacity = MAX_CAPACITY,
                CapacityIncreaseDuration = CAPACITY_INCREASE_DURATION
            };
        }
    }
}