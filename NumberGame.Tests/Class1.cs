using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberGame.Tests
{
    [TestFixture]
    public class Class1
    {
        [Test]
        public void TestMethod()
        {
            var entity = new NumberGame.Class1();

            Assert.That(entity.Method1(4), Is.EqualTo(5));
        }
    }
}
