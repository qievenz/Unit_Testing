﻿using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestNinja.Mocking;
using TestNinjaCore.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class BookingHelperTests_OverlappingBookingExistTests
    {
        private Booking _existingBooking;
        private Mock<IBookingRepository> _repository = new Mock<IBookingRepository>();

        [SetUp]
        public void Setup()
        {
            _existingBooking = new Booking
            {
                ArrivalDate = ArriveOn(2017, 1, 15),
                DepartureDate = DepartOn(2017, 1, 20),
                Reference = "a",
                Id = 2
            }
            ;
            _repository.Setup(r => r.GetActiveBookings(1)).Returns(new List<Booking>
            {
                _existingBooking
            }.AsQueryable());
        }

        [Test]
        public void BookingStartsAndFinishesBeforeAnExistingBooking_ReturnEmptyString()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = Before(_existingBooking.ArrivalDate, days: 2),
                DepartureDate = Before(_existingBooking.ArrivalDate),
            }, _repository.Object);

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void BookingStartsAndFinishesInTheMiddleAnExistingBooking_ReturnExistingReference()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = Before(_existingBooking.ArrivalDate),
                DepartureDate = After(_existingBooking.ArrivalDate),
            }, _repository.Object);

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void BookingStartsBeforeAndFinishesAfterAnExistingBooking_ReturnExistingReference()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = Before(_existingBooking.ArrivalDate),
                DepartureDate = After(_existingBooking.DepartureDate),
            }, _repository.Object);

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void BookingStartsAndFinishesInTheMiddleOfAnExistingBooking_ReturnExistingReference()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = After(_existingBooking.ArrivalDate),
                DepartureDate = Before(_existingBooking.DepartureDate),
            }, _repository.Object);

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void BookingStartsAndFinishesInTheMiddleOfAnExistingBookingButFinishesAfter_ReturnExistingReference()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = After(_existingBooking.ArrivalDate),
                DepartureDate = After(_existingBooking.DepartureDate),
            }, _repository.Object);

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void BookingStartsAndFinishesAfterAnExistingBooking_ReturnEmptyString()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = After(_existingBooking.DepartureDate),
                DepartureDate = After(_existingBooking.DepartureDate, days: 2),
            }, _repository.Object);

            Assert.That(result, Is.Empty);
        }
        [Test]
        public void BookingOverlapButNewBookingIsCanceled_ReturnEmptyString()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = After(_existingBooking.ArrivalDate),
                DepartureDate = After(_existingBooking.DepartureDate),
                Status = "Cancelled"
            }, _repository.Object);

            Assert.That(result, Is.Empty);
        }

        private DateTime Before(DateTime dateTime, int days = 1)
        {
            return dateTime.AddDays(-days);
        }
        private DateTime After(DateTime dateTime, int days = 1)
        {
            return dateTime.AddDays(+days);
        }

        private DateTime ArriveOn(int year, int month, int day)
        {
            return new DateTime(year, month, day, 14, 0, 0);
        }

        private DateTime DepartOn(int year, int month, int day)
        {
            return new DateTime(year, month, day, 10, 0, 0);
        }
    }
}
