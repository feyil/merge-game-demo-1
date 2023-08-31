using System.Collections;
using _game.Scripts.Components.Grid.Objects.Data;
using _game.Scripts.Components.Grid.Objects.View;
using _game.Scripts.Core;
using UnityEngine;

namespace _game.Scripts.Components.Grid.Objects
{
    public class ProducerGridObject : CommonGridObject
    {
        private readonly ProducerGridObjectView _viewSpecific;
        private readonly ProducerGridObjectData _data;
        private readonly WaitForSeconds _capacityIncreaseDuration;
        private Coroutine _capacityCoroutine;

        public ProducerGridObject(GridManager gridManager, GridCell gridCell, ProducerGridObjectView viewPrefab,
            ProducerGridObjectData data) : base(gridManager, gridCell, viewPrefab.gameObject)
        {
            _data = data;
            _viewSpecific = _view.GetComponent<ProducerGridObjectView>();
            _viewSpecific.Render(_data);
            _capacityIncreaseDuration = new WaitForSeconds(_data.CapacityIncreaseDuration);
            _capacityCoroutine = GameManager.Instance.StartCoroutine(IncreaseCapacity());
        }

        public override IItemData GetData()
        {
            return _data;
        }

        public override bool CanMerge(IGridObject gridObject)
        {
            return false;
        }

        public override bool Merge(IGridObject gridObject)
        {
            return false;
        }

        public override void OnInteract()
        {
            var center = _gridCell.GetCord();
            if (_data.Capacity <= 0) return;

            var spawnPoint = FindSpawnPoint(center);

            if (spawnPoint == null) return;
            var spawnCord = spawnPoint.GetCord();

            GridObjectSpawner.Instance.SpawnApplianceGridObject(_gridManager, spawnCord.x, spawnCord.y,
                ApplianceGridObjectData.GetRandomData());

            _data.Capacity--;
            if (_data.Capacity != 0)
            {
                _capacityCoroutine ??= GameManager.Instance.StartCoroutine(IncreaseCapacity());
                return;
            }

            Destroy();
            spawnPoint = _gridManager.GetRandomEmptyCell();
            if (spawnPoint == null) return;
            spawnCord = spawnPoint.GetCord();

            GridObjectSpawner.Instance.SpawnProducerGridObject(_gridManager, spawnCord.x, spawnCord.y,
                ProducerGridObjectData.GetDefaultData());
        }

        public override void Destroy()
        {
            base.Destroy();
            GameManager.Instance.StopCoroutine(_capacityCoroutine);
        }

        private IEnumerator IncreaseCapacity()
        {
            while (_data.Capacity != ProducerGridObjectData.MAX_CAPACITY)
            {
                yield return _capacityIncreaseDuration;
                var newCapacity = _data.Capacity + 1;
                _data.Capacity = Mathf.Clamp(newCapacity, 0, ProducerGridObjectData.MAX_CAPACITY);
                // Debug.Log($"New Capacity {_data.Capacity}");
            }

            _capacityCoroutine = null;
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