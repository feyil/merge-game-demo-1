using System.Collections.Generic;
using _game.Scripts.Components.Grid;
using _game.Scripts.Components.Grid.Objects;

namespace _game.Scripts.Components.TaskSystem.Data
{
    public class GridTask
    {
        private readonly TaskManager _taskManager;
        private readonly TaskData _taskData;

        private readonly List<ApplianceGridObject> _satisfiedList;

        public GridTask(TaskManager taskManager, TaskData taskData)
        {
            _satisfiedList = new List<ApplianceGridObject>();
            _taskManager = taskManager;
            _taskData = taskData;
        }

        public bool IsCompletable()
        {
            return _satisfiedList.Count != 0;
        }

        public void OnGridObjectAdded(IGridObject gridObject)
        {
            var applianceObject = gridObject as ApplianceGridObject;
            if (applianceObject == null) return;

            if (applianceObject.GetNumber() == GetNumber() && !_satisfiedList.Contains(applianceObject))
            {
                _satisfiedList.Add(applianceObject);
            }
        }

        public void OnGridObjectRemoved(IGridObject gridObject)
        {
            var applianceObject = gridObject as ApplianceGridObject;
            if (applianceObject == null) return;

            if (applianceObject.GetNumber() == GetNumber() && _satisfiedList.Contains(applianceObject))
            {
                _satisfiedList.Remove(applianceObject);
            }
        }

        public int GetNumber()
        {
            return _taskData.TargetLevel;
        }

        public void Complete()
        {
            var applianceObject = _satisfiedList[0];
            _satisfiedList.Remove(applianceObject);
            applianceObject.Destroy();
            _taskManager.CompleteTask(this);
        }

        public TaskData GetData()
        {
            return _taskData;
        }
    }
}