using OneSplash.Domain.Interfaces;

namespace OneSplash.Infrastructure.Shared.DateTime
{
    public class DateTimeService : IDateTimeService
    {
        public System.DateTime NowUtc => System.DateTime.UtcNow;
    }
}
