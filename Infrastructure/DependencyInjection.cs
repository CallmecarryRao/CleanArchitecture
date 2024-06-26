using Common.Entities;
using Common.Global;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using SqlSugar;

namespace Infrastructure;

using Common.ConfigOptions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddSugarSqlDB(this IServiceCollection service, IConfiguration configuration,
        IHostEnvironment hostEnvironment)
    {
        List<ConnectionConfig> lcc = new List<ConnectionConfig>();
        var dbConnectionConfig = configuration.GetSection("DataBaseConnection").Get<DataBaseConnection>();
        if (dbConnectionConfig != null)
        {
            //查找可用数据库
            var useConnect = dbConnectionConfig.ConnectionItem.Where(e => e.Enabled == true).ToList();

            if (useConnect != null)
            {
                foreach (var u in useConnect)
                {
                    var master = new ConnectionConfig()
                    {
                        ConfigId = u.Id,
                        ConnectionString = u.ConnectionString,
                        DbType = (DbType)u.DbType,
                        IsAutoCloseConnection = true
                    };
                    if (u.DbType == (int)DataBaseType.Sqlite)
                    {
                        master.ConnectionString = "DataSource=" + Path.Combine(hostEnvironment.ContentRootPath,
                            master.ConnectionString ?? string.Empty);
                    }

                    if (u.IsSalves)
                    {
                        var slaves = u.Salves.Where(e => e.Enabled == true).Select(e =>
                            new SlaveConnectionConfig
                            {
                                HitRate = e.HitRate,
                                ConnectionString = e.ConnectionString
                            }
                        ).ToList();

                        if (slaves != null)
                        {
                            master.SlaveConnectionConfigs = slaves;
                        }
                    }

                    lcc.Add(master);
                }

                var sugar = new SqlSugarScope(lcc
                    , options =>
                    {
                        useConnect?.ForEach(config =>
                        {
                            var sugarScopeProvider = options.GetConnection(config.Id);
                            sugarScopeProvider.QueryFilter.AddTableFilter<ISoftDeleted>(x => x.IsDeleted == false);
                        });
                    });

                service.AddSingleton<ISqlSugarClient>(sugar);
            }
        }

        return service;
    }
}