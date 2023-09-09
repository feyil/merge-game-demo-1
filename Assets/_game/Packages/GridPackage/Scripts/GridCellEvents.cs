using System;

namespace _game.Packages.GridPackage.Scripts
{
    [Serializable]
    public class GridCellEvents
    {
        public Action<GridCell> OnCellEnter;
        public Action<GridCell> OnCellExit;
        public Action<GridCell, int> OnCellClick;
    }
}