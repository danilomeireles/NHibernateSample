using NHibernateSample.OrmConfig;
using NHibernateSample.OrmConfig.Enums;

namespace NHibernateSample.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            NHibernateManager.Initialize(ShowSql.FormattedSql);
            NHibernateManager.UpdateSchema();
            //NHibernateManager.CreateSchema();
            //NHibernateManager.GenerateScriptFile("C:\\scripts", true);
            //NHibernateManager.DropSchema();
        }
    }
}
