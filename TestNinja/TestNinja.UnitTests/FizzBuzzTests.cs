using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    class FizzBuzzTests
    {
        [Test]
        public void GetOutput_MultiplosDe3_DevuelveFizz()
        {
            var result = FizzBuzz.GetOutput(3);

            Assert.That(result, Is.EqualTo("Fizz"));
        }

        [Test]
        public void GetOutput_MultiplosDe5_DevuelveBuzz()
        {
            var result = FizzBuzz.GetOutput(5);

            Assert.That(result, Is.EqualTo("Buzz"));
        }

        [Test]
        public void GetOutput_MultiplosDe3o5o15_DevuelveFizzBuzz()
        {
            var result = FizzBuzz.GetOutput(15);

            Assert.That(result, Is.EqualTo("FizzBuzz"));
        }
    }
}
