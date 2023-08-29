using _game.Scripts.Components.Grid.Objects.Data;
using _game.Scripts.Components.Grid.Objects.View;
using _game.Scripts.Utility;
using UnityEngine;

namespace _game.Scripts.Components.Grid.Objects
{
    public class GridObjectSpawner : MonoSingleton<GridObjectSpawner>
    {
        [SerializeField] private ApplianceGridObjectView m_applianceGridObjectView;
        [SerializeField] private ProducerGridObjectView m_producerGridObjectViewPrefab;

        public ApplianceGridObject SpawnApplianceGridObject(GridManager gridManager, int x, int y,
            ApplianceGridObjectData data)
        {
            var gridCell = gridManager.GetCell(x, y);
            var applianceGridObject = new ApplianceGridObject(gridManager, gridCell, m_applianceGridObjectView, data);
            gridCell.SetGridObject(applianceGridObject);
            return applianceGridObject;
        }

        public ProducerGridObject SpawnProducerGridObject(GridManager gridManager, int x, int y,
            ProducerGridObjectData data)
        {
            var gridCell = gridManager.GetCell(x, y);
            var producerGridObject =
                new ProducerGridObject(gridManager, gridCell, m_producerGridObjectViewPrefab, data);
            gridCell.SetGridObject(producerGridObject);
            return producerGridObject;
        }
    }
}