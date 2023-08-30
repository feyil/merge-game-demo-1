using System.Collections.Generic;
using _game.Scripts.Components.Grid.Objects;
using _game.Scripts.Components.Grid.Objects.Data;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _game.Scripts.Components.Grid.Data
{
    public class GridSaveManager : MonoBehaviour
    {
        private GridManager _gridManager;
        private string _prefKey;

        [Button]
        public void Initialize(GridManager gridManager, string prefKey)
        {
            _gridManager = gridManager;
            _prefKey = prefKey;
        }

        [Button]
        public void SaveGrid()
        {
            var gridData = new GridData()
            {
                CellData = new List<CellData>()
            };
            var gridCellList = _gridManager.GetAllCells();
            foreach (var gridCell in gridCellList)
            {
                var cellData = SaveCell(gridCell);
                gridData.CellData.Add(cellData);
            }

            var jsonData = JsonUtility.ToJson(gridData);
            PlayerPrefs.SetString(_prefKey, jsonData);
        }

        [Button]
        public bool LoadGrid()
        {
            var rawData = PlayerPrefs.GetString(_prefKey);
            if (rawData == null) return false;

            var gridData = JsonUtility.FromJson<GridData>(rawData);
            if (gridData == null) return false;

            foreach (var cellData in gridData.CellData)
            {
                switch (cellData.ItemType)
                {
                    case nameof(ProducerGridObject):
                    {
                        var cord = cellData.Cord;
                        var producerData = JsonUtility.FromJson<ProducerGridObjectData>(cellData.Data);
                        GridObjectSpawner.Instance.SpawnProducerGridObject(_gridManager, cord.x, cord.y, producerData);
                        break;
                    }
                    case nameof(ApplianceGridObject):
                    {
                        var cord = cellData.Cord;
                        var applianceData = JsonUtility.FromJson<ApplianceGridObjectData>(cellData.Data);
                        GridObjectSpawner.Instance.SpawnApplianceGridObject(_gridManager, cord.x, cord.y, applianceData);
                        break;
                    }
                }
            }

            return true;
        }

        [Button]
        private CellData SaveCell(GridCell gridCell)
        {
            var cord = gridCell.GetCord();

            var gridObject = gridCell.GetGridObject();
            if (gridObject != null)
            {
                var itemType = gridObject.GetType().Name;
                var data = gridObject.GetData();
                var rawData = JsonUtility.ToJson(data);

                var cellData = new CellData()
                {
                    Cord = cord,
                    ItemType = itemType,
                    Data = rawData
                };

                return cellData;
            }

            return new CellData()
            {
                Cord = cord
            };
        }

        private void OnApplicationQuit()
        {
            SaveGrid();
        }
    }
}