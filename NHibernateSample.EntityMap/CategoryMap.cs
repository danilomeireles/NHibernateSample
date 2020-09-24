using FluentNHibernate.Mapping;
using NHibernateSample.Entity;
using NHibernateSample.Util.Helpers;

namespace NHibernateSample.EntityMap
{
    public class CategoryMap : ClassMap<Category>
    {
        public CategoryMap()
        {
            Schema(ConfigurationHelper.DatabaseSchema);
            Id(x => x.Id).GeneratedBy.Sequence(SequenceNameHelper.GetSequenceName(GetType()));
            Map(x => x.Name).Not.Nullable().Length(50);
        }
    }
}
