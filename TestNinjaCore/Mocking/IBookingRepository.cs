using System.Linq;
using TestNinja.Mocking;

namespace TestNinjaCore.Mocking
{
    public interface IBookingRepository
    {
        IQueryable<Booking> GetActiveBookings(int? excludedBookingId = null);
    }
}