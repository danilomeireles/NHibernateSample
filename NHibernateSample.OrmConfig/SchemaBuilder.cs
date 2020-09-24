using NHibernate.Tool.hbm2ddl;
using NHibernateSample.OrmConfig.Enums;

namespace NHibernateSample.OrmConfig
{
    public static class SchemaBuilder
    {
        public static void BuildSchema(NHibernate.Cfg.Configuration config, BuildSchemaType buildSchemaType)
        {
            switch (buildSchemaType)
            {
                case BuildSchemaType.CreateSchema:
                    {
                        var schemaExport = new SchemaExport(config);
                        schemaExport.Create(false, true);
                        break;
                    }

                case BuildSchemaType.UpdateSchema:
                    {
                        var schemaUpdate = new SchemaUpdate(config);
                        schemaUpdate.Execute(false, true);
                        break;
                    }

                case BuildSchemaType.DropSchema:
                    {
                        var schemaExport = new SchemaExport(config);
                        schemaExport.Drop(false, true);
                        break;
                    }
            }
        }
    }
}