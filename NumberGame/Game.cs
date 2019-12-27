using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumberGame
{
    public sealed class Game : IGame
    {
        //TODO : replace
        #region cntr

        private const uint Width = 9;
        private const uint Height = 3;

        private static uint[] defaultValues = new uint[]
        {
            1, 2, 3, 4, 5, 6, 7, 8, 9,
            1, 1, 1, 2, 1, 3, 1, 4, 1,
            5, 1, 6, 1, 7, 1, 8
        };

        public static Game CreateDefault(Func<Cell[][], (bool, CellTuple)> next)
        {
            var cells = GenerateCells();
            return new Game(next, cells);
        }

        //TODO : so bad method
        private static Cell[][] GenerateCells()
        {
            var values = new Queue<uint>(defaultValues);

            var cells = new Cell[Height][];
            for (var i = 0; i < cells.Length; ++i)
            {
                cells[i] = new Cell[Width];
                for (var j = 0; j < cells[i].Length && values.Count > 0; ++j)
                    cells[i][j] = new Cell(values.Dequeue());
            }

            return cells;
        }

        #endregion

        private readonly LinkedList<Cell[]> cells;
        private readonly Func<Cell[][], (bool, CellTuple)> findNext;

        private uint iteration;

        public Game(Func<Cell[][], (bool, CellTuple)> findNext, Cell[][] input)
        {
            this.findNext = findNext;
            this.cells = new LinkedList<Cell[]>();
            for (var i = 0; i < input.Length; ++i)
            {
                cells.AddLast(new Cell[input[0].Length]);
                for (var j = 0; j < input[i].Length; ++j)
                    cells.Last.Value[j] = input[i][j];
            }
        }

        public IGame Next()
        {
            (var resolved, var tuple) = findNext(ToCells());

            if (resolved)
            {
                CloseCells(tuple);
            }
            else
            {
                UpdateCells();
            }
            ++iteration;
            return this; //TODO: wtf?
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

        public Cell[][] ToCells()
        {
            return cells.ToArray(); //TODO : check immutable
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            var stat = ToStatistics();
            sb.AppendLine($"Step: {stat.Iterations}");
            sb.AppendLine($"Total: {stat.Total}");
            sb.AppendLine($"Closed: {stat.Closed} ({stat.Closed * 100.0 / stat.Total} %)");
            return sb.ToString();
        }

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

            return new Statistics
            {
                Iterations = iteration,
                Total = (uint)total,
                Closed = (uint)closed,
            };
        }
    }
}
