using System;
using System.Collections.Generic;
using _game.Scripts.Components.Grid;
using _game.Scripts.Components.Grid.Objects;
using _game.Scripts.Components.Grid.Objects.Data;
using _game.Scripts.Components.TaskSystem.Data;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

namespace _game.Scripts.Components.TaskSystem
{
    public class TaskManager : MonoBehaviour
    {
        private static readonly string TASK_DATA_PREF_KEY = "task_data";

        public event Action<List<GridTask>> OnRefresh;

        private List<GridTask> _gridTaskList;
        private GridManager _gridManager;

        [Button]
        public void Initialize(GridManager gridManager)
        {
            _gridTaskList = new List<GridTask>();
            _gridManager = gridManager;

            LoadTasks();
        }

        [Button]
        public void OnGridObjectAdded(IGridObject gridObject)
        {
            foreach (var gridTask in _gridTaskList)
            {
                gridTask.OnGridObjectAdded(gridObject);
            }

            OnRefresh?.Invoke(_gridTaskList);
        }

        [Button]
        public void OnGridObjectRemoved(IGridObject gridObject)
        {
            foreach (var gridTask in _gridTaskList)
            {
                gridTask.OnGridObjectRemoved(gridObject);
            }

            OnRefresh?.Invoke(_gridTaskList);
        }

        private void LoadTasks()
        {
            var rawTaskPref = PlayerPrefs.GetString(TASK_DATA_PREF_KEY);
            if (rawTaskPref.IsNullOrWhitespace())
            {
                GenerateDefaultTasks();
                return;
            }

            var taskPref = JsonUtility.FromJson<TaskPref>(rawTaskPref);
            foreach (var taskData in taskPref.TaskDataList)
            {
                var gridTask = new GridTask(this, taskData);
                _gridTaskList.Add(gridTask);
            }
        }

        private void GenerateDefaultTasks()
        {
            for (var i = 0; i < 2; i++)
            {
                var task = GenerateTask();
                if (task == null) continue;

                _gridTaskList.Add(task);
            }
        }

        private GridTask GenerateTask()
        {
            var maxNumber = FindMaxNumber() * 2;
            if (maxNumber > ApplianceGridObjectData.MAX_VALUE) return null;

            foreach (var gridTask in _gridTaskList)
            {
                if (gridTask.GetNumber() == maxNumber)
                {
                    maxNumber *= 2;
                }
            }

            return new GridTask(this, new TaskData()
            {
                TargetLevel = maxNumber
            });
        }

        private int FindMaxNumber()
        {
            var maxNumber = ApplianceGridObjectData.MIN_VALUE;
            var allGridCells = _gridManager.GetAllCells();
            foreach (var gridCell in allGridCells)
            {
                var gridObject = gridCell.GetGridObject();
                if (gridObject == null) continue;

                var applianceObject = gridObject as ApplianceGridObject;
                if (applianceObject == null) continue;

                var number = applianceObject.GetNumber();
                if (number > maxNumber)
                {
                    maxNumber = number;
                }
            }

            return maxNumber;
        }

        public void CompleteTask(GridTask gridTask)
        {
            var index = _gridTaskList.IndexOf(gridTask);
            var task = GenerateTask();
            if (task != null)
            {
                _gridTaskList[index] = task;
            }
            else
            {
                _gridTaskList.Remove(gridTask);
            }

            OnRefresh?.Invoke(_gridTaskList);
            SaveTasks(_gridTaskList);
        }

        private void SaveTasks(List<GridTask> gridTaskList)
        {
            var taskPref = new TaskPref()
            {
                TaskDataList = new List<TaskData>()
            };
            foreach (var gridTask in gridTaskList)
            {
                var data = gridTask.GetData();
                taskPref.TaskDataList.Add(data);
            }

            var rawData = JsonUtility.ToJson(taskPref);
            PlayerPrefs.SetString(TASK_DATA_PREF_KEY, rawData);
        }
    }
}