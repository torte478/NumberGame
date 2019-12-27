using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumberGame
{
    /// <summary>
    /// Implementation of general game logic
    /// </summary>
    public sealed class Game : IGame
    {
        #region cntr
        private static uint[][] defaultValues = new uint[3][]
        {
            new uint[] {1, 2, 3, 4, 5, 6, 7, 8, 9 },
            new uint[] {1, 1, 1, 2, 1, 3, 1, 4, 1 },
            new uint[] {5, 1, 6, 1, 7, 1, 8 }
        };

        /// <summary>
        /// Create game with default start values
        /// </summary>
        /// <param name="next">Function for next step search</param>
        public static Game CreateDefault(Func<ICells, (bool, CellTuple)> next)
        {
            var cells = GenerateCells();
            return new Game(next, Cells.Create, cells);
        }

        private static Cell[][] GenerateCells()
        {
            var cells = new Cell[defaultValues.Length][];
            for (var i = 0; i < cells.Length; ++i)
            {
                cells[i] = new Cell[defaultValues[i].Length];
                for (var j = 0; j < cells[i].Length; ++j)
                    cells[i][j] = new Cell(defaultValues[i][j]);
            }
            return cells;
        }

        #endregion

        private readonly LinkedList<Cell[]> cells;
        private readonly Func<Cell[][], ICells> toCells;
        private readonly Func<ICells, (bool, CellTuple)> findNext;
        
        private uint iteration;
        private uint deletions;

        /// <summary>
        /// Implementation of general game logic
        /// </summary>
        /// <param name="findNext">function for next step search</param>
        /// <param name="toCells">factory method for createing ICells instances</param>
        /// <param name="input">start field values</param>
        public Game(
            Func<ICells, (bool, CellTuple)> findNext,
            Func<Cell[][], ICells> toCells,
            Cell[][] input)
        {
            this.findNext = findNext;
            this.toCells = toCells;

            this.cells = new LinkedList<Cell[]>();
            for (var i = 0; i < input.Length; ++i)
            {
                cells.AddLast(new Cell[input[0].Length]);
                for (var j = 0; j < input[i].Length; ++j)
                    cells.Last.Value[j] = input[i][j];
            }
        }

        /// <summary>
        /// Make next step
        /// </summary>
        public void Next()
        {
            if (cells.Count == 0)
                return;

            (var resolved, var tuple) = findNext(ToCells());

            if (resolved)
            {
                CloseCells(tuple);
                RemoveFullRows();
            }
            else
            {
                UpdateCells();
            }
            ++iteration;
        }

        private void RemoveFullRows()
        {
            var removed = cells
                            .Where(row => row.All(cell => cell.Closed))
                            .ToArray();
            foreach (var row in removed)
            {
                cells.Remove(row);
                ++deletions;
            }
        }

        private void UpdateCells()
        {
            var opened = GetOpenedCells();
            while (opened.Count > 0)
            {
                var last = cells.Last.Value;
                var isComplete = last[last.Length - 1].Empty;
                if (last[last.Length - 1].Empty)
                {
                    var start = 0;
                    for (; !last[start].Empty; ++start) ;
                    FillRow(last, opened, start);
                }
                else
                {
                    var row = new Cell[last.Length];
                    FillRow(row, opened);
                    cells.AddLast(row);
                }
            }
        }

        private static void FillRow(Cell[] row, Queue<uint> values, int start = 0)
        {
            for (var j = start; j < row.Length && values.Count > 0; ++j)
            {
                var value = values.Dequeue();
                row[j] = new Cell(value);
            }
        }

        private void CloseCells(CellTuple tuple)
        {
            cells.ElementAt((int)tuple.First.Item1)[tuple.First.Item2].Close();
            cells.ElementAt((int)tuple.Second.Item1)[tuple.Second.Item2].Close();
            cells.ElementAt((int)tuple.Second.Item1)[tuple.Second.Item2].Close();
        }
        private Queue<uint> GetOpenedCells()
        {
            var opened = cells
                         .SelectMany(row => row
                                            .Where(cell => !cell.Closed && !cell.Empty )
                                            .Select(cell => cell.Value))
                         .ToArray();
            return new Queue<uint>(opened);
        }

        /// <summary>
        /// Get cells of game field
        /// </summary>
        public ICells ToCells()
        {
            var values = cells
                            .Select(row => row.ToArray())
                            .ToArray();
            return toCells(values);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            var stat = ToStatistics();
            sb.AppendLine($"Step: {stat.Iterations}");
            sb.AppendLine($"Deleted: {stat.Deletions}");
            sb.AppendLine($"Total: {stat.Total}");
            sb.AppendLine($"Closed: {stat.Closed} ({stat.Closed * 100.0 / stat.Total} %)");
            return sb.ToString();
        }

        /// <summary>
        /// Get statistics of current game
        /// </summary>
        public Statistics ToStatistics()
        {
            var total = 0;
            var closed = 0;
            foreach (var row in cells)
                foreach (var cell in row)
                {
                    if (cell.Empty) break;

                    ++total;
                    if (cell.Closed)
                        ++closed;
                }

            //hack
            if (total == 0)
            {
                total = 1;
                closed = 1;
            }

            return new Statistics
            {
                Iterations = iteration,
                Deletions = deletions,
                Total = (uint)total,
                Closed = (uint)closed,
            };
        }
    }
}
