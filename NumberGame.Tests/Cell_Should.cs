using NUnit.Framework;

namespace NumberGame.Tests
{
    [TestFixture]
    internal sealed class Cell_Should
    {
        [Test]
        public void BeClosed_AfterDefaultCreate()
        {
            var cell = new Cell();

            Assert.That(cell.Closed, Is.True);
        }

        [Test]
        public void BeOpened_AfterCreateWithValue()
        {
            var cell = new Cell(5);

            Assert.That(cell.Closed, Is.False);
        }
    }
}
