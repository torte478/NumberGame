using NUnit.Framework;

namespace NumberGame.Tests
{
    [TestFixture]
    internal sealed class Game_Should
    {
        private Game game;
        private Cell[][] cells;

        [SetUp]
        public void SetUp()
        {
            game = Game.CreateDefault((_) => new CellTuple());
            cells = game.ToCells();
        }

        [Test]
        public void CreateDefaultCells_AfterCreate()
        {
            Assert.That(cells.Length, Is.EqualTo(3));
        }

        [Test]
        [TestCase(0, 0, 1, TestName = "First cell is 1")]
        [TestCase(2, 6, 8, TestName = "Last cell is 8")]
        public void FillDefatultCells_AfterCreate(int row, int column, int expected)
        {
            var actual = cells[row][column].Value;
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
