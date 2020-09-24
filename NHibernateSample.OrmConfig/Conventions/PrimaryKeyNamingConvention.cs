using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;
using System.Text.RegularExpressions;

namespace NHibernateSample.OrmConfig.Conventions
{
    public class PrimaryKeyNamingConvention : IIdConvention
    {
        private const string PascalCaseRegex = @"(?<!_)([A-Z])";

        public void Apply(IIdentityInstance instance)
        {
            var pkName = Regex.Replace(instance.EntityType.Name, PascalCaseRegex, "_$1").Remove(0, 1).ToLower();
            pkName = $"pk_{pkName}";
            instance.Column(pkName);
        }
    }
}