namespace NumberGame
{
    public sealed class FirstStepAlgorithm
    {
        private readonly Cell[][] cells;

        public FirstStepAlgorithm(Cell[][] cells)
        {
            this.cells = cells;
        }

        public (bool, CellTuple) Resolve()
        {
            for (uint i = 0; i < cells.Length; ++i)
                for (uint j = 0; j < cells[i].Length; ++j)
                {
                    var current = (i, j);
                    var horizontal = FindHorizontal(current);
                    var vertical = FindVertical(current);

                    if (IsMatch(current, horizontal))
                        return BuildResult(current, horizontal);
                    else if (IsMatch(current, vertical))
                        return BuildResult(current, vertical);
                }

            return (false, new CellTuple());
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

        private static (bool, CellTuple) BuildResult((uint, uint) first, (uint, uint) second)
        {
            return (true, new CellTuple()
            {
                First = first,
                Second = second,
            });
        }
    }
}
