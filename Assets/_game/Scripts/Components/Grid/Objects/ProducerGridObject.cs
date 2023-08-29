using _game.Scripts.Components.Grid.Objects.Data;
using _game.Scripts.Components.Grid.Objects.View;
using UnityEngine;

namespace _game.Scripts.Components.Grid.Objects
{
    public class ProducerGridObject : CommonGridObject
    {
        private readonly ProducerGridObjectView _viewSpecif;
        private readonly ProducerGridObjectData _data;

        public ProducerGridObject(GridManager gridManager, GridCell gridCell, GameObject viewPrefab,
            ProducerGridObjectData data) : base(gridManager, gridCell, viewPrefab)
        {
            _data = data;
            _viewSpecif = _view.GetComponent<ProducerGridObjectView>();
            _viewSpecif.Render(_data);
        }
    }
}