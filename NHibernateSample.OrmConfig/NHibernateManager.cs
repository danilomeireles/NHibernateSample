using System;
using FluentNHibernate.Cfg;
using NHibernate;
using System.Linq;
using NHibernateSample.OrmConfig.Conventions;
using System.IO;
using System.Reflection;
using NHibernateSample.OrmConfig.Enums;
using NHibernateSample.Util.Helpers;

namespace NHibernateSample.OrmConfig
{
    public static class NHibernateManager
    {
        private static ISessionFactory _currentSession { get; set; }        

        private static FluentConfiguration _fluentConfiguration { get; set; }

        public static void Initialize(ShowSql showSql)
        {
            _fluentConfiguration = Fluently.Configure()
                .Database(DatabaseConfigurer.GetDatabaseConfig(showSql))
                .Mappings(m =>
                {
                    m.FluentMappings
                        .AddFromAssembly(Assembly.Load(ConfigurationHelper.EntitiesMapsNamespace))                        
                        .Conventions.Add(new TableNamingConvention())
                        .Conventions.Add(new ColumnNamingConvention())
                        .Conventions.Add(new PrimaryKeyNamingConvention())
                        .Conventions.Add(new ForeignKeyNamingConvention())
                        .Conventions.Add(new ReferenceConvention());
                })
                .ExposeConfiguration(x =>
                {
                    x.SetInterceptor(new SqlStatementInterceptor());
                });

            _currentSession = _fluentConfiguration.BuildSessionFactory();
        }

        public static ISession OpenSession()
        {
            return _currentSession.OpenSession();
        }

        public static void CreateSchema()
        {
            if (_fluentConfiguration == null)
                throw new Exception($"{nameof(NHibernateManager)}.{nameof(CreateSchema)} error: {nameof(_fluentConfiguration)} is null. You should initialize the orm before.");

            _fluentConfiguration.ExposeConfiguration(x => SchemaBuilder.BuildSchema(x, BuildSchemaType.CreateSchema));
            _currentSession = _fluentConfiguration.BuildSessionFactory();
        }

        public static void UpdateSchema()
        {
            if (_fluentConfiguration == null)
                throw new Exception($"{nameof(NHibernateManager)}.{nameof(UpdateSchema)} error: {nameof(_fluentConfiguration)} is null. You should initialize the orm before.");

            _fluentConfiguration.ExposeConfiguration(x => SchemaBuilder.BuildSchema(x, BuildSchemaType.UpdateSchema));
            _currentSession = _fluentConfiguration.BuildSessionFactory();
        }

        public static void DropSchema()
        {
            if (_fluentConfiguration == null)
                throw new Exception($"{nameof(NHibernateManager)}.{nameof(DropSchema)} error: {nameof(_fluentConfiguration)} is null. You should initialize the orm before.");

            _fluentConfiguration.ExposeConfiguration(x => SchemaBuilder.BuildSchema(x, BuildSchemaType.DropSchema));
            _currentSession = _fluentConfiguration.BuildSessionFactory();
        }

        public static string GenerateScriptFile(string scriptsDirectoryPath, bool openScriptFileAfterCreation)
        {
            if (_fluentConfiguration == null)
                throw new Exception($"{nameof(NHibernateManager)}.{nameof(GenerateScriptFile)} error: {nameof(_fluentConfiguration)} is null. You should initialize the orm before.");

            _fluentConfiguration.ExposeConfiguration(x => ScriptFileBuilder.GenerateScriptsFile(x, scriptsDirectoryPath, openScriptFileAfterCreation));
            _currentSession = _fluentConfiguration.BuildSessionFactory();

            var directory = new DirectoryInfo(scriptsDirectoryPath);
            var latestCreateFile = directory.GetFiles().OrderByDescending(p => p.CreationTime).FirstOrDefault();
            var content = File.ReadAllText(latestCreateFile.FullName);
            return content;
        }        
    }
}