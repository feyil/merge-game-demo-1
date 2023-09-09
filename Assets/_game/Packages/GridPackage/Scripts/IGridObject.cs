using _game.Packages.GridPackage.Scripts.Objects.Data;
using UnityEngine;

namespace _game.Packages.GridPackage.Scripts
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