using System;
using NUnit.Framework;

namespace NumberGame.Tests
{
    [TestFixture]
    internal sealed class FirstStepAlgorithm_Should
    {
        private static Cell[][] cells;

        [Test]
        public void CloseCell_OnVertical()
        {
            BuildCells(new[]
            {
                "12",
                "13",
            });

            CheckSolution((0, 0), (1, 0));
        }

        [Test]
        public void CloseCell_OnHorizontal()
        {
            BuildCells(new[]
            {
                "11",
                "13",
            });

            CheckSolution((0, 0), (0, 1));
        }

        [Test]
        public void NotCloseCell_WhenHaveObstacle()
        {
            BuildCells(new[]
            {
                "121",
                "133",
            });

            CheckSolution((0, 0), (1, 0));
        }

        [Test]
        public void CloseCell_OnNextLine()
        {
            BuildCells(new[]
            {
                "1##",
                "#1#",
            });

            CheckSolution((0, 0), (1, 1));
        }

        [Test]
        public void CloseCell_WhenSumIsTen()
        {
            BuildCells(new[]
            {
                "19",
                "22",
            });

            CheckSolution((0, 0), (0, 1));
        }

        [Test]
        public void NotCloseCell_WhenStepsNotExists()
        {
            BuildCells(new[]
            {
                "12",
                "34"
            });

            var (resolved, _) = new FirstStepAlgorithm(cells).Resolve();

            Assert.That(resolved, Is.False);
        }

        private static void CheckSolution((uint, uint) first, (uint, uint) second)
        {
            var (_, next) = new FirstStepAlgorithm(cells).Resolve();

            Assert.That(next.First.Item1, Is.EqualTo(first.Item1), () => "(x, _) (_, _)");
            Assert.That(next.First.Item2, Is.EqualTo(first.Item2), () => "(_, x) (_, _)");
            Assert.That(next.Second.Item1, Is.EqualTo(second.Item1), () => "(_, _) (x, _)");
            Assert.That(next.Second.Item2, Is.EqualTo(second.Item2), () => "(_, _) (_, x)");
        }

        private static void BuildCells(string[] values)
        {
            cells = new Cell[values.Length][];
            for (var i = 0; i < values.Length; ++i)
            {
                cells[i] = new Cell[values[i].Length];
                for (var j = 0; j < values[i].Length; ++j)
                    cells[i][j] = ToCell(values[i][j]);
            }
        }

        private static Cell ToCell(char ch)
        {
            if (ch == '#')
            {
                return Cell.CreateClosed();
            }
            else
            {
                var value = (uint)Char.GetNumericValue(ch);
                return Cell.Create(value);
            }
        }
    }
}
