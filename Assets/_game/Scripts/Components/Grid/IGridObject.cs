using UnityEngine;

namespace _game.Scripts.Components.Grid
{
    public interface IGridObject
    {
        void SetPosition(Vector3 position);
        bool CanMerge(IGridObject gridObject);
        void Merge(IGridObject gridObject);
        void Destroy();
    }
}