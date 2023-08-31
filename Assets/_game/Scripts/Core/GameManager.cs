using System.Collections.Generic;
using _game.Scripts.Components.Grid;
using _game.Scripts.Components.Grid.Data;
using _game.Scripts.Components.Grid.Objects;
using _game.Scripts.Components.Grid.Objects.Data;
using _game.Scripts.Components.TaskSystem;
using _game.Scripts.Components.TaskSystem.Data;
using _game.Scripts.Core.Ui;
using _game.Scripts.Ui;
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
        [SerializeField] private TaskManager m_taskManager;

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

            m_taskManager.Initialize(gridManager);

            var gameEventManager = GameEventManager.Instance;
            gameEventManager.OnGridObjectAdded -= m_taskManager.OnGridObjectAdded;
            gameEventManager.OnGridObjectAdded += m_taskManager.OnGridObjectAdded;

            gameEventManager.OnGridObjectRemoved -= m_taskManager.OnGridObjectRemoved;
            gameEventManager.OnGridObjectRemoved += m_taskManager.OnGridObjectRemoved;
            
            m_taskManager.OnRefresh -= OnRefreshTaskView;
            m_taskManager.OnRefresh += OnRefreshTaskView;
            
            var isLoaded = m_saveManager.LoadGrid();
            if (!isLoaded)
            {
                InitializeGameGrid(gridManager);
            }
        }

        private void OnRefreshTaskView(List<GridTask> taskList)
        {
            var taskViewController = UiManager.Get<GameUiController>().GetTaskViewController();
            taskViewController.Render(taskList);
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