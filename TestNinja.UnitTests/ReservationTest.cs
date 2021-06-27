using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class ReservationTest
    {
        [Test]
        public void CanBeCancelledBy_AdminIsCancelling_ReturnsTrue()
        {
            var reservation = new Reservation();

            var result = reservation.CanBeCancelledBy(new User { IsAdmin = true });

            Assert.That(result, Is.True);
        }

        [Test]
        public void CanBeCancelledBy_NotAdminIsCancelling_ReturnsFalse()
        {
            var reservation = new Reservation();

            var result = reservation.CanBeCancelledBy(new User { IsAdmin = false });

            Assert.IsFalse(result);
        }

        [Test]
        public void CanBeCancelledBy_UserIsCancelling_ReturnsTrue()
        {
            var userMadeBy = new User { IsAdmin = false };
            var reservation = new Reservation { MadeBy = userMadeBy };

            var result = reservation.CanBeCancelledBy(userMadeBy);

            Assert.IsTrue(result);
        }

        [Test]
        public void CanBeCancelledBy_AnotherUserIsCancelling_ReturnsFalse()
        {
            var user_1 = new User();
            var user_2 = new User();
            var reservation = new Reservation { MadeBy = user_1 };

            var result = reservation.CanBeCancelledBy(user_2);

            Assert.IsFalse(result);
        }
    }
}
