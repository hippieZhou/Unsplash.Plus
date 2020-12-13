﻿using System;
using System.Diagnostics;
using FreeSql;
using Microsoft.Extensions.Options;
using OneSplash.Domain.Interfaces;
using OneSplash.Domain.Settings;

namespace OneSplash.Infrastructure.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        private readonly ApiSettings _options;
        private readonly IDateTimeService _dateTime;
        private readonly IFreeSql _freeSql;

        public ApplicationDbContext(IOptions<ApiSettings> options, IDateTimeService dateTime)
        {
            _options = options.Value ?? throw new ArgumentNullException(nameof(options));
            _dateTime = dateTime ?? throw new ArgumentNullException(nameof(dateTime));

            _freeSql = new FreeSqlBuilder()
                .UseConnectionString(DataType.Sqlite, $"data source=sample.db")
                .UseAutoSyncStructure(true) //自动同步实体结构到数据库
                .Build().SetDbContextOptions(opt => { opt.OnEntityChange = report => Trace.WriteLine(report); });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder) => builder.UseFreeSql(_freeSql);

        protected override void OnModelCreating(ICodeFirst codefirst)
        {
            codefirst.IsLazyLoading = true;
            base.OnModelCreating(codefirst);
        }
    }
}
