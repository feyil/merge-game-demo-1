using System.Collections;
using _game.Scripts.Components.Grid.Objects;
using _game.Scripts.Components.Grid.Objects.Data;
using _game.Scripts.Core.Ui;
using _game.Scripts.Ui.Controllers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _game.Scripts.Core
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject m_appliancePrefab;
        [SerializeField] private GameObject m_producerPrefab;

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
            var gridCell = gridManager.GetCell(0, 0);
            gridCell.SetGridObject(new ApplianceGridObject(gridManager, gridCell, m_appliancePrefab,
                new ApplianceGridObjectData()
                {
                    Number = 2
                }));

            gridCell = gridManager.GetCell(2, 0);
            gridCell.SetGridObject(new ApplianceGridObject(gridManager, gridCell, m_appliancePrefab,
                new ApplianceGridObjectData()
                {
                    Number = 4
                }));

            gridCell = gridManager.GetCell(4, 0);
            gridCell.SetGridObject(new ApplianceGridObject(gridManager, gridCell, m_appliancePrefab,
                new ApplianceGridObjectData()
                {
                    Number = 2
                }));


            gridCell = gridManager.GetCell(6, 0);
            gridCell.SetGridObject(new ProducerGridObject(gridManager, gridCell, m_producerPrefab,
                new ProducerGridObjectData()
                {
                }));
        }
    }
}