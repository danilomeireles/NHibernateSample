using NHibernate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NHibernateSample.Repository.Interface
{
    public interface IGenericRepository<T>
    {
        Task<T> Find(int id);
        Task<IList<T>> FindAll(string orderByProperty = null, int? maxResults = null);
        Task<IList<T>> FindAllByProperty(string property, object value, string orderByProperty = null, int? maxResults = null);
        Task<T> FindOneByProperty(string property, object value, string orderByProperty = null);
        Task<IList<T>> FindAllByCriteria(ICriteria criteria);
        Task<IList<T>> FindAllByPropertyILike(string property, string value, string orderByProperty = null, int? maxResults = null);
        Task Create(T instance);
        Task Update(T instance);
        Task Delete(T instance);
    }
}