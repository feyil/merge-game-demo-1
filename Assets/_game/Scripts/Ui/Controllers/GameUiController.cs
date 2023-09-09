using _game.Packages.GridPackage.Scripts;
using _game.Scripts.Core.Ui;
using UnityEngine;
using UnityEngine.UI;

namespace _game.Scripts.Ui.Controllers
{
    public class GameUiController : UiController
    {
        [SerializeField] private Button m_inventoryButton;
        [SerializeField] private GridManager m_gridManager;
        [SerializeField] private TaskViewController m_taskViewController;

        public override void Show()
        {
            base.Show();
            m_gridManager.SpawnGrid(_canvas);

            m_inventoryButton.onClick.RemoveAllListeners();
            m_inventoryButton.onClick.AddListener(() => { UiManager.Get<InventoryUiController>().Show(); });
        }

        public GridManager GetGridManager()
        {
            return m_gridManager;
        }

        public TaskViewController GetTaskViewController()
        {
            return m_taskViewController;
        }
    }
}