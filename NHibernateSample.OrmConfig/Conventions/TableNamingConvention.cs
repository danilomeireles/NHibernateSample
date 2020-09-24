using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;
using System.Text.RegularExpressions;

namespace NHibernateSample.OrmConfig.Conventions
{
    public class TableNamingConvention : IClassConvention
    {
        private const string PascalCaseRegex = @"(?<!_)([A-Z])";

        public void Apply(IClassInstance instance)
        {
            var tablePrefix = string.Empty;
            var tableName = $"{tablePrefix}{Regex.Replace(instance.EntityType.Name, PascalCaseRegex, "_$1").Remove(0, 1)}".ToLower();
            instance.Table(tableName);
        }
    }
}