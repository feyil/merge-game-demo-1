using _game.Scripts.Components.Grid.Objects;
using _game.Scripts.Components.Grid.Objects.Data;
using _game.Scripts.Core.Ui;
using _game.Scripts.Ui.Controllers;
using _game.Scripts.Utility;
using Sirenix.OdinInspector;

namespace _game.Scripts.Core
{
    public class GameManager : MonoSingleton<GameManager>
    {
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
            
            GridObjectSpawner.Instance.SpawnApplianceGridObject(gridManager, 0, 0,
                new ApplianceGridObjectData() { Number = 2 });
            
            GridObjectSpawner.Instance.SpawnApplianceGridObject(gridManager, 2, 0,
                new ApplianceGridObjectData() { Number = 4 });
            
            GridObjectSpawner.Instance.SpawnApplianceGridObject(gridManager, 4, 0,
                new ApplianceGridObjectData() { Number = 2 });

            GridObjectSpawner.Instance.SpawnProducerGridObject(gridManager, 6, 0, ProducerGridObjectData.GetDefaultData());
        }
    }
}