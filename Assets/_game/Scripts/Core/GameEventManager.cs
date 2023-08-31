using System;
using _game.Scripts.Components.Grid;
using _game.Scripts.Utility;

namespace _game.Scripts.Core
{
    //TODO it can be implemented more generic and flexible but for now lets stick with this
    public class GameEventManager : MonoSingleton<GameEventManager>
    {
        public event Action<IGridObject> OnGridObjectAdded;
        public event Action<IGridObject> OnGridObjectRemoved;

        public void TriggerOnGridObjectAdded(IGridObject gridObject)
        {
            OnGridObjectAdded?.Invoke(gridObject);
        }

        public void TriggerOnGridObjectRemoved(IGridObject gridObject)
        {
            OnGridObjectRemoved?.Invoke(gridObject);
        }
    }
}