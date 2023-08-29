using _game.Scripts.Components.Grid.Objects.Data;
using _game.Scripts.Components.Grid.Objects.View;
using UnityEngine;

namespace _game.Scripts.Components.Grid.Objects
{
    public class ApplianceGridObject : CommonGridObject
    {
        private readonly ApplianceGridObjectData _data;
        private readonly ApplianceGridObjectView _viewSpecif;

        public ApplianceGridObject(GridManager gridManager, GridCell gridCell, GameObject viewPrefab,
            ApplianceGridObjectData data) : base(gridManager, gridCell, viewPrefab)
        {
            _data = data;
            _viewSpecif = _view.GetComponent<ApplianceGridObjectView>();
            Refresh();
        }

        private void Refresh()
        {
            _viewSpecif.Render(_data);
        }

        public override bool CanMerge(IGridObject gridObject)
        {
            if (gridObject is not ApplianceGridObject applianceGridObject) return false;

            return applianceGridObject._data.Number == _data.Number;
        }

        public override void Merge(IGridObject gridObject)
        {
            var applianceGridObject = gridObject as ApplianceGridObject;
            if (applianceGridObject == null) return;

            _data.Number += applianceGridObject._data.Number;
            gridObject.Destroy();
            Refresh();
        }
    }
}