using _game.Scripts.Components.Grid.Objects.Data;
using _game.Scripts.Components.Grid.Objects.View;
using UnityEngine;

namespace _game.Scripts.Components.Grid.Objects
{
    public class ProducerGridObject : CommonGridObject
    {
        private readonly ProducerGridObjectView _viewSpecif;
        private readonly ProducerGridObjectData _data;

        public ProducerGridObject(GridManager gridManager, GridCell gridCell, ProducerGridObjectView viewPrefab,
            ProducerGridObjectData data) : base(gridManager, gridCell, viewPrefab.gameObject)
        {
            _data = data;
            _viewSpecif = _view.GetComponent<ProducerGridObjectView>();
            _viewSpecif.Render(_data);
        }

        public override bool CanMerge(IGridObject gridObject)
        {
            return false;
        }

        public override void Merge(IGridObject gridObject)
        {
            throw new System.NotImplementedException();
        }

        public override void OnInteract()
        {
            Debug.Log($"Interacted {_view.name}");
        }
    }
}