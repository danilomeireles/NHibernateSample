using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;
using System.Text.RegularExpressions;

namespace NHibernateSample.OrmConfig.Conventions
{
    public class ForeignKeyNamingConvention : IReferenceConvention
    {
        private const string PascalCaseRegex = @"(?<!_)([A-Z])";

        public void Apply(IManyToOneInstance instance)
        {
            var destinationEntity = Regex.Replace(instance.EntityType.Name, PascalCaseRegex, "_$1").Remove(0, 1).ToLower();
            var destinationProperty = Regex.Replace(instance.Property.Name, PascalCaseRegex, "_$1").Remove(0, 1).ToLower();

            var fkColumnName = $"fk_{destinationProperty}".ToLower();
            instance.Column(fkColumnName);

            var fkConstraintName = $"fk_{destinationProperty}_on_{destinationEntity}".ToLower();
            instance.ForeignKey(fkConstraintName);
        }
    }
}