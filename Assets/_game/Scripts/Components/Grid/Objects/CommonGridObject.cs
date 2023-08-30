using _game.Scripts.Components.Grid.Objects.Data;
using UnityEngine;

namespace _game.Scripts.Components.Grid.Objects
{
    public abstract class CommonGridObject : IGridObject
    {
        protected readonly GridManager _gridManager;
        protected readonly GameObject _view;

        protected GridCell _gridCell;

        protected CommonGridObject(GridManager gridManager, GridCell gridCell, GameObject viewPrefab)
        {
            _gridManager = gridManager;
            _gridCell = gridCell;

            _view = Object.Instantiate(viewPrefab, gridManager.GetObjectContainer());
            _view.GetComponent<RectTransform>().sizeDelta = gridCell.GetSize();
        }

        public abstract IItemData GetData();

        public abstract bool CanMerge(IGridObject gridObject);
        public abstract bool Merge(IGridObject gridObject);

        public abstract void OnInteract();

        public void UpdateCell(GridCell gridCell)
        {
            _gridCell = gridCell;
        }

        public void SetPosition(Vector3 position)
        {
            // if (_view == null) return;
            _view.transform.SetAsLastSibling();
            _view.transform.position = position;
        }

        public virtual void Destroy()
        {
            _gridCell.SetGridObject(null);
            UpdateCell(null);
            Object.Destroy(_view);
        }
    }
}