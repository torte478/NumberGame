using System;

namespace NumberGame
{
    public sealed class FirstStepAlgorithm : IAlgorithm
    {
        private readonly Func<Cell[][], ILogic> getLogic; //TODO : rename
        private readonly Cell[][] cells;

        public FirstStepAlgorithm(Func<Cell[][], ILogic> getLogic, Cell[][] cells)
        {
            this.getLogic = getLogic;
            this.cells = cells;
        }

        public (bool, CellTuple) Resolve()
        {
            var logic = getLogic(cells);
            for (uint i = 0; i < cells.Length; ++i)
                for (uint j = 0; j < cells[i].Length; ++j)
                {
                    var current = (i, j);
                    var horizontal = logic.FindHorizontalFor(current);
                    var vertical = logic.FindVerticalFor(current);

                    if (horizontal.Item1)
                        return horizontal;
                    else if (vertical.Item1)
                        return vertical;
                }

            return (false, new CellTuple()); //TODO : what are idiom
        }
    }
}
