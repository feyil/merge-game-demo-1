using _game.Scripts.Components.Grid;
using _game.Scripts.Core.Ui;
using UnityEngine;

namespace _game.Scripts.Ui.Controllers
{
    public class GameUiController : UiController
    {
        [SerializeField] private GridManager m_gridManager;
        [SerializeField] private TaskViewController m_taskViewController;

        public override void Show()
        {
            base.Show();
            m_gridManager.SpawnGrid(_canvas);
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
