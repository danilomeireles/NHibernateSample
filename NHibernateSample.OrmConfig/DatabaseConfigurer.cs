using FluentNHibernate.Cfg.Db;
using System;
using NHibernateSample.Util.Helpers;
using NHibernateSample.OrmConfig.Enums;

namespace NHibernateSample.OrmConfig
{
    public class DatabaseConfigurer
    {
        public static IPersistenceConfigurer GetDatabaseConfig(ShowSql showSql)
        {
            return showSql switch
            {
                ShowSql.No => PostgreSQLConfiguration.Standard.ConnectionString(ConfigurationHelper.ConnectionString),
                ShowSql.UnformattedSql => PostgreSQLConfiguration.Standard.ConnectionString(ConfigurationHelper.ConnectionString).ShowSql(),
                ShowSql.FormattedSql => PostgreSQLConfiguration.Standard.ConnectionString(ConfigurationHelper.ConnectionString).ShowSql().FormatSql(),
                _ => throw new Exception($"Database configuration not found on {nameof(DatabaseConfigurer)}.{nameof(GetDatabaseConfig)}."),
            };
        }
    }
}