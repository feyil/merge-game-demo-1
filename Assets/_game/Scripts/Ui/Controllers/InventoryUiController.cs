using _game.Scripts.Core.Ui;
using UnityEngine;
using UnityEngine.UI;

namespace _game.Scripts.Ui.Controllers
{
    public class InventoryUiController : UiController
    {
        [SerializeField] private Button m_exitButton;

        public override void Show()
        {
            m_exitButton.onClick.RemoveAllListeners();
            m_exitButton.onClick.AddListener(Hide);

            base.Show();
        }
    }
}