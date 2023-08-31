using System.Collections.Generic;
using _game.Scripts.Components.TaskSystem;
using _game.Scripts.Components.TaskSystem.Data;
using UnityEditor;
using UnityEngine;

namespace _game.Scripts.Ui
{
    public class TaskViewController : MonoBehaviour
    {
        [SerializeField] private TaskView[] m_taskViewList;

        public void Render(List<GridTask> taskList)
        {
            for (var index = 0; index < m_taskViewList.Length; index++)
            {
                var taskView = m_taskViewList[index];
                if (index >= taskList.Count)
                {
                    taskView.SetActive(false);
                    continue;
                }

                var gridTask = taskList[index];
                taskView.Render(gridTask);
            }
        }
    }
}