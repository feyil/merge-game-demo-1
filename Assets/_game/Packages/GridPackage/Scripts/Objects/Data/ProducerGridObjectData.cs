using System;

namespace _game.Packages.GridPackage.Scripts.Objects.Data
{
    [Serializable]
    public class ProducerGridObjectData : IItemData
    {
        public static int MAX_CAPACITY = 10;
        public static int CAPACITY_INCREASE_DURATION = 30;
        
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