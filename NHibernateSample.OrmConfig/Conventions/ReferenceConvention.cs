using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace NHibernateSample.OrmConfig.Conventions
{
    public class ReferenceConvention : IReferenceConvention, IHasManyConvention
    {
        public void Apply(IManyToOneInstance instance)
        {
            instance.Fetch.Join(); // disable lazy loading
        }

        public void Apply(IOneToManyCollectionInstance instance)
        {
            instance.Fetch.Select(); // disable lazy loading
        }
    }
}
