using _game.Scripts.Components.Grid;
using _game.Scripts.Components.Grid.Objects;
using UnityEngine;

namespace _game.Scripts.Components.InventorySystem
{
    public class InventoryManager : MonoBehaviour
    {
        public void Initialize()
        {
        }
        
        public void AddItem(IGridObject gridObject)
        {
            var applianceGridObject = gridObject as ApplianceGridObject;
            if (applianceGridObject == null) return;
            
            applianceGridObject.Destroy();
        }
    }
}