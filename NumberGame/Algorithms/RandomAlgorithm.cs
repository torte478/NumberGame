using System;
using System.Collections.Generic;

namespace NumberGame.Algorithms
{
    public sealed class RandomAlgorithm : IAlgorithm
    {
        private readonly Random random;
        private readonly Func<Cell[][], ILogic> getLogic;
        private readonly Cell[][] cells;

        public RandomAlgorithm(Func<Cell[][], ILogic> getLogic, Cell[][] cells)
        {
            random = new Random((int)DateTime.Now.Ticks);
            this.getLogic = getLogic;
            this.cells = cells;
        }

        public (bool, CellTuple) Resolve()
        {
            var steps = FindAllAvailableSteps();
            if (steps.Count > 0)
            {
                var index = random.Next(steps.Count);
                return (true, steps[index]);
            }
            else
                return (false, new CellTuple());
        }

        private List<CellTuple> FindAllAvailableSteps()
        {
            var available = new List<CellTuple>();

            var logic = getLogic(cells);
            for (uint i = 0; i < cells.Length; ++i)
                for (uint j = 0; j < cells[i].Length; ++j)
                {
                    var current = (i, j);

                    var horizontal = logic.FindHorizontalFor(current);
                    if (horizontal.Item1)
                        available.Add(horizontal.Item2);

                    var vertical  = logic.FindVerticalFor(current);
                    if (vertical.Item1)
                        available.Add(vertical.Item2);
                }

            return available;
        }
    }
}