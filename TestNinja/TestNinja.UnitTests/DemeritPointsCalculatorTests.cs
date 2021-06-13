using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    class DemeritPointsCalculatorTests
    {
        private DemeritPointsCalculator _demeritCalc;

        [SetUp]
        public void SetUp()
        {
            _demeritCalc = new DemeritPointsCalculator();
        }

        [Test]
        [TestCase(-1)]
        [TestCase(301)]
        public void CalculateDemeritPoints_WhenSpeedOutOfRange_ThrowsException(int speed)
        {
            Assert.That(() => _demeritCalc.CalculateDemeritPoints(speed), Throws.Exception.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        [TestCase(1)]
        [TestCase(30)]
        [TestCase(65)]
        public void CalculateDemeritPoints_WhenSpeedBetweenLimit_ReturnZero(int speed)
        {
            var result = _demeritCalc.CalculateDemeritPoints(speed);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateDemeritPoints_When5kmSpeedOver65Limit_Return1Point()
        {
            var result = _demeritCalc.CalculateDemeritPoints(65+5);

            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void CalculateDemeritPoints_When10kmSpeedOver65Limit_Return2Point()
        {
            var result = _demeritCalc.CalculateDemeritPoints(65+5+5);

            Assert.That(result, Is.EqualTo(2));
        }
    }
}
