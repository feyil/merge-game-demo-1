using _game.Scripts.Components.Grid;
using _game.Scripts.Components.Grid.Data;
using _game.Scripts.Components.Grid.Objects;
using _game.Scripts.Components.Grid.Objects.Data;
using _game.Scripts.Core.Ui;
using _game.Scripts.Ui.Controllers;
using _game.Scripts.Utility;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _game.Scripts.Core
{
    public class GameManager : MonoSingleton<GameManager>
    {
        [SerializeField] private int m_startProducerCount;
        [SerializeField] private GridSaveManager m_saveManager;

        private void Awake()
        {
            InitializeAwake();
        }

        private void Start()
        {
            InitializeStart();
        }

        private void InitializeAwake()
        {
            UiManager.Instance.Initialize();
        }

        private void InitializeStart()
        {
            StartGame();
        }

        [Button]
        private void StartGame()
        {
            var gameUiController = UiManager.Get<GameUiController>();
            gameUiController.Show();
            
            var gridManager = gameUiController.GetGridManager();
            m_saveManager.Initialize(gridManager, "grid_json_data");

            var isLoaded = m_saveManager.LoadGrid();
            if (!isLoaded)
            {
                InitializeGameGrid(gridManager);    
            }
        }

        private void InitializeGameGrid(GridManager gridManager)
        {
            for (var i = 0; i < m_startProducerCount; i++)
            {
                var spawnPoint = gridManager.GetRandomEmptyCell();
                var spawnCord = spawnPoint.GetCord();

                GridObjectSpawner.Instance.SpawnProducerGridObject(gridManager, spawnCord.x, spawnCord.y,
                    ProducerGridObjectData.GetDefaultData());
            }
        }
    }
}