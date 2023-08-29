using _game.Scripts.Components.Grid;
using _game.Scripts.Core.Ui;
using UnityEngine;

namespace _game.Scripts.Ui.Controllers
{
    public class GameUiController : UiController
    {
        [SerializeField] private GridManager m_gridManager;

        public override void Show()
        {
            m_gridManager.SpawnGrid();
            base.Show();
        }

        public GridManager GetGridManager()
        {
            return m_gridManager;
        }
    }
}
