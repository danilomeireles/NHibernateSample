using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;
using System.Text.RegularExpressions;

namespace NHibernateSample.OrmConfig.Conventions
{
    public class ColumnNamingConvention : IPropertyConvention
    {
        private const string PascalCaseRegex = @"(?<!_)([A-Z])";

        public void Apply(IPropertyInstance instance)
        {
            var columnName = Regex.Replace(instance.Name, PascalCaseRegex, "_$1").Remove(0, 1).ToLower();
            instance.Column(columnName);
        }
    }
}