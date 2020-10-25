using OneSplash.Application.Interfaces;
using System;
using FreeSql;

namespace OneSplash.Infrastructure.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IFreeSql _fsql;
        private readonly IDateTimeService _dateTime;

        public ApplicationDbContext(IFreeSql fsql, IDateTimeService dateTime)
        {
            _fsql = fsql ?? throw new ArgumentNullException(nameof(fsql));
            _dateTime = dateTime ?? throw new ArgumentNullException(nameof(dateTime));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder) => builder.UseFreeSql(_fsql);

        protected override void OnModelCreating(ICodeFirst codefirst)
        {
            codefirst.IsLazyLoading = true;
            base.OnModelCreating(codefirst);
        }
    }
}
