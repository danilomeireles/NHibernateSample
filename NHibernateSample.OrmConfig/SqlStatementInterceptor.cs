using NHibernate;
using NHibernate.SqlCommand;
using System.Diagnostics;

namespace NHibernateSample.OrmConfig
{
    public class SqlStatementInterceptor : EmptyInterceptor
    {
        public override SqlString OnPrepareStatement(SqlString sql)
        {
            Debug.WriteLine($"NHibernate: [{sql}]");
            return sql;
        }
    }
}
