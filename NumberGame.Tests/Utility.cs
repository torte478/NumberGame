using System;

namespace NumberGame.Tests
{
    internal static class Utility
    {
        public static Cell[][] BuildCells(string[] values)
        {
            var cells = new Cell[values.Length][];
            for (var i = 0; i < values.Length; ++i)
            {
                cells[i] = new Cell[values[i].Length];
                for (var j = 0; j < values[i].Length; ++j)
                    cells[i][j] = ToCell(values[i][j]);
            }

            return cells;
        }

        private static Cell ToCell(char ch)
        {
            if (ch == '_')
            {
                return new Cell();
            }
            else if (ch == '#')
            {
                var cell = new Cell(uint.MaxValue);
                cell.Close();
                return cell;
            }
            else
            {
                var value = (uint)Char.GetNumericValue(ch);
                return new Cell(value);
            }
        }
    }
}
