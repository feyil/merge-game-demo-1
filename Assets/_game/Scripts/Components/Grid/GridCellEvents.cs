using System;

namespace _game.Scripts.Components.Grid
{
    [Serializable]
    public class GridCellEvents
    {
        public Action<GridCell> OnCellEnter;
        public Action<GridCell> OnCellExit;
        public Action<GridCell, int> OnCellClick;
    }
}