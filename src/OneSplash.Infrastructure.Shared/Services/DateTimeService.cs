using OneSplash.Application.Interfaces;
using System;

namespace OneSplash.Shared.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.UtcNow;
    }
}
