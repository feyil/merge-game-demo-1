using System;
using _game.Scripts.Components.Grid.Objects.Data;
using _game.Scripts.Components.Grid.Objects.View;
using _game.Scripts.Components.TaskSystem;
using _game.Scripts.Components.TaskSystem.Data;
using UnityEngine;
using UnityEngine.UI;

namespace _game.Scripts.Ui
{
    public class TaskView : MonoBehaviour
    {
        [SerializeField] private Button m_actionButton;
        [SerializeField] private ApplianceGridObjectView m_applianceGridObjectView;

        public void Render(GridTask gridTask)
        {
            SetActive(true);

            var isCompletable = gridTask.IsCompletable();
            m_actionButton.gameObject.SetActive(isCompletable);
            m_actionButton.onClick.RemoveAllListeners();
            m_actionButton.onClick.AddListener(() =>
            {
                if (gridTask.IsCompletable())
                {
                    gridTask.Complete();
                }
            });

            var number = gridTask.GetNumber();
            m_applianceGridObjectView.Render(new ApplianceGridObjectData()
            {
                Number = number
            });
        }

        public void SetActive(bool state)
        {
            m_actionButton.gameObject.SetActive(state);
            m_applianceGridObjectView.gameObject.SetActive(state);
        }
    }
}