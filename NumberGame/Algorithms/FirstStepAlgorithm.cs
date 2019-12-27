namespace NumberGame.Algorithms
{
    public sealed class FirstStepAlgorithm : IAlgorithm
    {
        private readonly bool horizontal;
        private readonly ICells cells;

        public FirstStepAlgorithm(bool horizontal, ICells cells)
        {
            this.horizontal = horizontal;
            this.cells = cells;
        }

        public (bool, CellTuple) Resolve()
        {
            for (uint i = 0; i < cells.Height; ++i)
                for (uint j = 0; j < cells.Width; ++j)
                {
                    var current = (i, j);
                    var hor = cells.FindHorizontalFor(current);
                    var vert = cells.FindVerticalFor(current);

                    (bool, CellTuple)[] neighbors;
                    if (horizontal)
                        neighbors = new[] { hor, vert };
                    else
                        neighbors = new[] { vert, hor };

                    foreach (var neighbor in neighbors)
                        if (neighbor.Item1)
                            return neighbor;
                }

            return (false, new CellTuple());
        }
    }
}
