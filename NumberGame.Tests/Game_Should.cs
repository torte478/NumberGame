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
            game = Game.CreateDefault();
            cells = game.ToCells();
        }

        [Test]
        public void CreateDefaultCells_AfterCreate()
        {
            Assert.That(cells.Length, Is.EqualTo(3));
        }

        [Test]
        [TestCase(0, 0, 1, TestName = "First cell")]
        [TestCase(2, 6, 8, TestName = "Last cell")]
        public void FillDefatultCells_AfterCreate(int row, int column, int expected)
        {
            var actual = cells[row][column].Value;
            Assert.That(actual, Is.EqualTo(expected));
        }

        //[Test]
        //public void CloseFirstCell_AfterFirstStep()
        //{
        //    var next = game.Next();

        //    Assert.That(next.ToCells()[0][0].Closed, Is.True);
        //}
    }
}
