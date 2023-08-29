using UnityEngine;

namespace _game.Scripts.Components.Grid.Objects
{
    public abstract class CommonGridObject : IGridObject
    {
        protected GridManager _gridManager;
        protected readonly GameObject _view;

        protected CommonGridObject(GridManager gridManager, GridCell gridCell, GameObject viewPrefab)
        {
            _gridManager = gridManager;

            _view = Object.Instantiate(viewPrefab, gridManager.GetObjectContainer());
            _view.GetComponent<RectTransform>().sizeDelta = gridCell.GetSize();
        }

        public void SetPosition(Vector3 position)
        {
            _view.transform.SetAsLastSibling();
            _view.transform.position = position;
        }

        public abstract bool CanMerge(IGridObject gridObject);
        public abstract void Merge(IGridObject gridObject);
        public void Destroy()
        {
            Object.Destroy(_view);
        }

        public abstract void OnInteract();
    }
}