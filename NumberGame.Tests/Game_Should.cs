﻿using NUnit.Framework;

namespace NumberGame.Tests
{
    [TestFixture]
    internal sealed class Game_Should
    {
        private Game game;
        private ICells cells;

        [SetUp]
        public void SetUp()
        {
            game = Game.CreateDefault((_) => (true, new CellTuple()));
            cells = game.ToCells();
        }

        [Test]
        public void CreateDefaultCells_AfterCreate()
        {
            Assert.That(cells.Height, Is.EqualTo(3));
        }

        [Test]
        [TestCase(0, 0, 1, TestName = "First cell is 1")]
        [TestCase(2, 6, 8, TestName = "Last cell is 8")]
        public void FillDefatultCells_AfterCreate(int row, int column, int expected)
        {
            var actual = cells.At(row,column).Value;
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

            game.Next();
            var next = game.ToCells();

            var actual = next.At(2, 1).Value;
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

            game.Next();
            var next = game.ToCells();

            var actual = next.At(2, 2).Empty;
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

            game.Next();
            var next = game.ToCells();

            var actual = next.At(1, 2).Value;
            Assert.That(actual, Is.EqualTo(1));
            Assert.That(next.At(2, 2).Empty, Is.True);
        }

        [Test]
        public void RemoveRow_WhenItFullyClosed()
        {
            var cells = Utility.BuildCells(new[]
            {
                "1#",
                "22",
                "#1"
            });
            var game = new Game(
                (_) => (true, new CellTuple { First = (1, 0), Second = (1, 1) }),
                Cells.Create,
                cells);

            game.Next();
            var next = game.ToCells();

            Assert.That(next.Height, Is.EqualTo(2));
        }

        private static Game CreateGame(string[] values)
        {
            var cells = Utility.BuildCells(values);
            var game = new Game(
                (_) => (false, new CellTuple()),
                Cells.Create,
                cells);
            return game;
        }
    }
}
