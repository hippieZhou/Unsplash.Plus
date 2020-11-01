using System;
using System.Collections.Generic;
using System.Text;

namespace OneSplash.Domain.Interfaces
{
    public interface IDateTimeService
    {
        DateTime NowUtc { get; }
    }
}
