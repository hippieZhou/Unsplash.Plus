using System;
using System.Diagnostics;
using FreeSql;
using Microsoft.Extensions.Options;
using OneSplash.Domain.Interfaces;
using OneSplash.Domain.Settings;

namespace OneSplash.Infrastructure.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        private readonly AppSettings _options;
        private readonly IDateTimeService _dateTime;
        private readonly IFreeSql _fsql;

        public ApplicationDbContext(IOptions<AppSettings> options,IDateTimeService dateTime)
        {
            _options = options.Value;
            _dateTime = dateTime ?? throw new ArgumentNullException(nameof(dateTime));

            _fsql = new FreeSqlBuilder()
              .UseConnectionString(DataType.Sqlite, _options.DbConnection)
              .UseAutoSyncStructure(true) //自动同步实体结构到数据库
              .Build();
            _fsql.SetDbContextOptions(opt =>
            {
                opt.OnEntityChange = report =>
                {
                    Trace.WriteLine(report);
                };
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder) => builder.UseFreeSql(_fsql);

        protected override void OnModelCreating(ICodeFirst codefirst)
        {
            codefirst.IsLazyLoading = true;
            base.OnModelCreating(codefirst);
        }
    }
}
