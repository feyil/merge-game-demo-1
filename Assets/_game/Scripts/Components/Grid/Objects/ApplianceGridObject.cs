using _game.Scripts.Components.Grid.Objects.Data;
using _game.Scripts.Components.Grid.Objects.View;
using UnityEngine;

namespace _game.Scripts.Components.Grid.Objects
{
    public class ApplianceGridObject : CommonGridObject
    {
        private readonly ApplianceGridObjectData _data;
        private readonly ApplianceGridObjectView _viewSpecific;

        public ApplianceGridObject(GridManager gridManager, GridCell gridCell, ApplianceGridObjectView viewPrefab,
            ApplianceGridObjectData data) : base(gridManager, gridCell, viewPrefab.gameObject)
        {
            _data = data;
            _viewSpecific = _view.GetComponent<ApplianceGridObjectView>();
            Refresh();
        }

        private void Refresh()
        {
            _viewSpecific.Render(_data);
        }

        public override IItemData GetData()
        {
            return _data;
        }

        public override bool CanMerge(IGridObject gridObject)
        {
            if (gridObject is not ApplianceGridObject applianceGridObject) return false;

            return applianceGridObject._data.Number == _data.Number;
        }

        public override bool Merge(IGridObject gridObject)
        {
            var applianceGridObject = gridObject as ApplianceGridObject;
            if (applianceGridObject == null) return false;

            var gridObjectNumber = applianceGridObject._data.Number;
            if (gridObjectNumber == ApplianceGridObjectData.MAX_VALUE) return false;

            var cord = _gridCell.GetCord();
            gridObject.Destroy();
            Destroy();
            
            GridObjectSpawner.Instance.SpawnApplianceGridObject(_gridManager, cord.x, cord.y,
                new ApplianceGridObjectData()
                {
                    Number = _data.Number * 2
                });
            
            return true;
        }

        public override void OnInteract()
        {
            if (_data.Number == ApplianceGridObjectData.MAX_VALUE)
            {
                Destroy();
            }
        }

        public int GetNumber()
        {
            return _data.Number;
        }

        public void RenderTaskTargetView()
        {
            _data.Color = Color.green;
            _viewSpecific.Render(_data);
        }
    }
}