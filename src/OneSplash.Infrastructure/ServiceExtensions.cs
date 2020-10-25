using FreeSql;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OneSplash.Application.Interfaces;
using OneSplash.Domain.Settings;
using OneSplash.Infrastructure.Contexts;
using OneSplash.Infrastructure.Repositories;
using OneSplash.Infrastructure.Shared.Unsplash;
using System;
using System.Diagnostics;

namespace OneSplash.Infrastructure
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(sp =>
            {
                IFreeSql fsql = new FreeSqlBuilder()
               .UseConnectionString(DataType.Sqlite, configuration.GetConnectionString("DefaultConnection"))
               .UseAutoSyncStructure(true) //自动同步实体结构到数据库
               .Build();
                fsql.SetDbContextOptions(opt =>
                {
                    opt.OnEntityChange = report =>
                    {
                        Trace.WriteLine(report);
                    };
                });
                return fsql;
            });
            services.Configure<AppSettings>(configuration.GetSection("CacheConfiguration"));
            services.AddSingleton<ApplicationDbContext>();
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddTransient<MemorySplashService>();
            services.AddTransient<UnSplashService>();
            services.AddTransient<Func<bool, ISplashService>>(serviceProvider => key =>
            {
                return key ? serviceProvider.GetService<MemorySplashService>() : serviceProvider.GetService<MemorySplashService>();
            });
            return services;
        }
    }
}
