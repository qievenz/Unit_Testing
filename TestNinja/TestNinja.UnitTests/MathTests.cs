using NUnit.Framework;
using System.Linq;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    class MathTests
    {
        private Math _math;

        [SetUp]
        public void SetUp()
        {
            _math = new Math();
        }
        [Test]
        [Ignore("Because is the shiieeet")]
        public void Add_WhenCalled_ReturnTheSumOfArgs()
        {
            var result = _math.Add(1, 2);

            Assert.That(result, Is.EqualTo(3));
        }

        [Test]
        [TestCase(2, 1, 2)]
        [TestCase(1, 2, 2)]
        [TestCase(1, 1, 1)]
        public void Max_WhenCalled_ReturnTheGreaterArg(int a, int b, int expected)
        {
            var result = _math.Max(a, b);

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void GetOddNumbers_LimitIsGreaterThanZero_ReturnOddNumbersUpToLimit()
        {
            var result = _math.GetOddNumbers(5);
            //Muy general
            Assert.That(result, Is.Not.Empty);
            //No tan general
            Assert.That(result.Count(), Is.EqualTo(5));
            //Mas especifico
            Assert.That(result, Does.Contain(1));
            Assert.That(result, Does.Contain(3));
            Assert.That(result, Does.Contain(5));
            //Especifico pero mas corto
            Assert.That(result, Is.EquivalentTo(new[] { 1, 3, 5 }));

            Assert.That(result, Is.Ordered);
            Assert.That(result, Is.Unique);
        }
    }
}
