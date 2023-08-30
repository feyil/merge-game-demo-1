using System;
using _game.Scripts.Components.Grid.Objects.Data;
using _game.Scripts.Components.Grid.Objects.View;
using UnityEngine;

namespace _game.Scripts.Components.Grid.Objects
{
    public class ProducerGridObject : CommonGridObject
    {
        private readonly ProducerGridObjectView _viewSpecific;
        private readonly ProducerGridObjectData _data;

        public ProducerGridObject(GridManager gridManager, GridCell gridCell, ProducerGridObjectView viewPrefab,
            ProducerGridObjectData data) : base(gridManager, gridCell, viewPrefab.gameObject)
        {
            _data = data;
            _viewSpecific = _view.GetComponent<ProducerGridObjectView>();
            _viewSpecific.Render(_data);
        }

        public override bool CanMerge(IGridObject gridObject)
        {
            return false;
        }

        public override void Merge(IGridObject gridObject)
        {
            throw new NotImplementedException();
        }

        public override void OnInteract()
        {
            var center = _gridCell.GetCord();
            var spawnPoint = FindSpawnPoint(center);

            if (spawnPoint == null) return;
            var spawnCord = spawnPoint.GetCord();

            GridObjectSpawner.Instance.SpawnApplianceGridObject(_gridManager, spawnCord.x, spawnCord.y,
                ApplianceGridObjectData.GetRandomData());
        }

        private GridCell FindSpawnPoint(Vector2Int center)
        {
            GridCell nearestPoint = null;
            var nearestDistanceSquared = float.MaxValue;

            var dimensions = _gridManager.GetDimensions();
            var maxDimension = Mathf.Max(dimensions.x, dimensions.y) * 1.5f;
            for (var range = 1; range < maxDimension; range++)
            {
                for (var x = center.x - range; x <= center.x + range; x++)
                {
                    for (var y = center.y - range; y <= center.y + range; y++)
                    {
                        var cell = _gridManager.GetCell(x, y);
                        if (cell == null || cell.IsFilled()) continue;

                        var distanceSquared = (x - center.x) * (x - center.x) +
                                              (y - center.y) * (y - center.y);

                        if (distanceSquared <= range * range && distanceSquared < nearestDistanceSquared)
                        {
                            nearestPoint = cell;
                            nearestDistanceSquared = distanceSquared;
                        }
                    }
                }

                if (range % 2 != 0 && nearestPoint != null) return nearestPoint;
            }

            return nearestPoint;
        }
    }
}