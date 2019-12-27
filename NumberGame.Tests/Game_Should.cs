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
            game = Game.CreateDefault((_) => (true, new CellTuple()));
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

        [Test]
        public void FillCells_WhenAvailableStepsNotExists()
        {
            var game = CreateGame(new[]
            {
                "1#",
                "#2"
            });

            var next = game.Next().ToCells();

            var actual = next[2][1].Value;
            Assert.That(actual, Is.EqualTo(2));
        }

        [Test]
        public void FillCells_WhenRowIsNotComplete()
        {
            var game = CreateGame(new[]
            {
                "1##",
                "#2#",
            });

            var next = game.Next().ToCells();

            var actual = next[2][2].Empty;
            Assert.That(actual, Is.True);
        }

        [Test]
        public void FillCells_WhenCurrentCellRowIsNotComplete()
        {
            var game = CreateGame(new[]
            {
                "1##",
                "#2_",
            });

            var next = game.Next().ToCells();

            var actual = next[1][2].Value;
            Assert.That(actual, Is.EqualTo(1));
            Assert.That(next[2][2].Empty, Is.True);
        }

        private static Game CreateGame(string[] values)
        {
            var cells = Utility.BuildCells(values);
            var game = new Game(
                (_) => (false, new CellTuple()),
                cells);
            return game;
        }
    }
}
