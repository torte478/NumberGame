namespace NumberGame
{
    public sealed class Logic : ILogic
    {
        private readonly Cell[][] cells;

        public Logic(Cell[][] cells)
        {
            this.cells = cells;
        }

        public (bool, CellTuple) FindVerticalFor((uint, uint) first)
        {
            var second = FindVertical(first);
            return CheckMatch(first, second);
        }

        public (bool, CellTuple) FindHorizontalFor((uint, uint) first)
        {
            var second = FindHorizontal(first);
            return CheckMatch(first, second);
        }

        private (bool, CellTuple) CheckMatch((uint, uint) first, (uint, uint) second)
        {
            if (IsMatch(first, second))
                return (true, new CellTuple()
            {
                First = first,
                Second = second,
            });
            else
                return (false, new CellTuple());
        }

        private static (bool, CellTuple) BuildResult((uint, uint) first, (uint, uint) second)
        {
            return (true, new CellTuple()
            {
                First = first,
                Second = second,
            });
        }

        private bool IsMatch((uint, uint) first, (uint, uint) second)
        {
            if (first.Item1 < 0 || first.Item1 > cells.Length
                || first.Item2 < 0 || first.Item2 > cells[0].Length
                || second.Item1 < 0 || second.Item1 > cells.Length
                || second.Item2 < 0 || second.Item2 > cells[0].Length)
                return false;

            var one = cells[first.Item1][first.Item2];
            var another = cells[second.Item1][second.Item2];

            return !one.Closed
                   && !another.Closed
                   && (one.Value == another.Value || one.Value + another.Value == 10);
        }

        private (uint, uint) FindVertical((uint row, uint column) current)
        {
            for (var i = current.row + 1; i < cells.Length; ++i)
            {
                var other = cells[i][current.column];
                if (other.Closed)
                    continue;
                return (i, current.column);
            }
            return (uint.MaxValue, uint.MaxValue);
        }

        private (uint, uint) FindHorizontal((uint row, uint column) current)
        {
            for (var i = current.row; i < cells.Length; ++i)
            {
                var start = (i == current.row) ? current.column + 1 : 0;
                for (var j = start; j < cells[current.row].Length; ++j)
                {
                    var other = cells[i][j];
                    if (other.Closed)
                        continue;
                    return (i, j);
                }
            }
            return (uint.MaxValue, uint.MaxValue);
        }
    }

}
