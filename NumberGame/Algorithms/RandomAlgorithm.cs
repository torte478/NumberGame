using System;
using System.Collections.Generic;

namespace NumberGame.Algorithms
{
    /// <summary>
    /// Algorithm, that set random from allowed steps
    /// </summary>
    public sealed class RandomAlgorithm : IAlgorithm
    {
        private readonly Random random;
        private readonly ICells cells;

        public RandomAlgorithm(ICells cells)
        {
            random = new Random((int)DateTime.Now.Ticks);
            this.cells = cells;
        }

        /// <summary>
        /// Find available step
        /// </summary>
        /// <returns>'true' and cuple of cell coordinates when step exists
        /// and 'false' and garbage on other case</returns>
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

            for (uint i = 0; i < cells.Height; ++i)
                for (uint j = 0; j < cells.Width; ++j)
                {
                    var current = (i, j);

                    var horizontal = cells.FindHorizontalFor(current);
                    if (horizontal.Item1)
                        available.Add(horizontal.Item2);

                    var vertical  = cells.FindVerticalFor(current);
                    if (vertical.Item1)
                        available.Add(vertical.Item2);
                }

            return available;
        }
    }
}