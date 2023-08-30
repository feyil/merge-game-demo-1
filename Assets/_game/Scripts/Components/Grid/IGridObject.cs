using _game.Scripts.Components.Grid.Objects.Data;
using UnityEngine;

namespace _game.Scripts.Components.Grid
{
    public interface IGridObject
    {
        void SetPosition(Vector3 position);
        bool CanMerge(IGridObject gridObject);
        bool Merge(IGridObject gridObject);
        void Destroy();
        void OnInteract();
        void UpdateCell(GridCell gridCell);
        IItemData GetData();
    }
}