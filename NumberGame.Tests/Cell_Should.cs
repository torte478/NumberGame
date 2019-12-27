using NUnit.Framework;

namespace NumberGame.Tests
{
    [TestFixture]
    internal sealed class Cell_Should
    {
        [Test]
        public void BeClosed_AfterDefaultCreate()
        {
            var cell = Cell.CreateClosed();

            Assert.That(cell.Closed, Is.True);
        }

        [Test]
        public void BeOpened_AfterCreateWithValue()
        {
            var cell = Cell.Create(5);

            Assert.That(cell.Closed, Is.False);
        }
    }
}
