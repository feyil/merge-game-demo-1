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
            _viewSpecif.Render(_data);
        }
    }
}