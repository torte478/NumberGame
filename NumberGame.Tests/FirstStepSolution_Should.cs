using NUnit.Framework;

namespace NumberGame.Tests
{
    [TestFixture]
    internal sealed class FirstStepSolution_Should
    {
        private static readonly uint[][] values = new[]
        {
            new uint[] {1, 2 },
            new uint[] {1, 2 },
        };

        private Cell[][] cells;

        [SetUp]
        public void SetUp()
        {
            cells = new Cell[values.Length][];
            for (var i = 0; i < values.Length; ++i)
            {
                cells[i] = new Cell[values[i].Length];
                for (var j = 0; j < values[i].Length; ++j)
                    cells[i][j] = Cell.Create(values[i][j]);
            }
        }

        [Test]
        public void CloseFirstCell_AfterFirstStep()
        {
            var next = FirstStepSolution.Solve(cells);

            Assert.That(next.First.Item1, Is.EqualTo(0));
            Assert.That(next.First.Item2, Is.EqualTo(0));
        }

        //[Test]
        //public void CloseNextCell_AfterSecondStep()
        //{
        //    var next = game.Next().Next().ToCells();

        //    var cell = next[1][1];
        //    Assert.That(cell.Closed, Is.True);
        //}
    }
}
