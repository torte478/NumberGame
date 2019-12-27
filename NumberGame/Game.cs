using System;
using System.Collections.Generic;

namespace NumberGame
{
    public sealed class Game : IGame
    {
        private const uint Width = 9;
        private const uint Height = 3;

        private static uint[] defaultValues = new uint[]
        {
            1, 2, 3, 4, 5, 6, 7, 8, 9,
            1, 1, 1, 2, 1, 3, 1, 4, 1,
            5, 1, 6, 1, 7, 1, 8
        };

        public static Game CreateDefault(Func<Cell[][], CellTuple> next)
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
                    cells[i][j] = Cell.Create(values.Dequeue());
            }

            return cells;
        }

        private readonly Cell[][] cells;
        private readonly Func<Cell[][], CellTuple> next;

        private Game(Func<Cell[][], CellTuple> next, Cell[][] cells)
        {
            this.next = next;
            this.cells = cells;
        }

        public IGame Next()
        {
            var tuple = next(cells);

            cells[tuple.First.Item1][tuple.First.Item2].Close();
            cells[tuple.Second.Item1][tuple.Second.Item2].Close();

            return this;
        }

        public Cell[][] ToCells()
        {
            return cells;
        }
    }
}
