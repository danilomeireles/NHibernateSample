using NHibernate;
using NHibernate.Criterion;
using NHibernateSample.OrmConfig;
using NHibernateSample.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NHibernateSample.Repository
{
    public class GenericRepository<T> : IGenericRepository<T>
    {
        public async Task<T> Find(int id)
        {
            using var session = NHibernateManager.OpenSession();
            return await session.GetAsync<T>(id);            
        }

        public async Task<IList<T>> FindAll(string orderByProperty = null, int? maxResults = null)
        {
            using var session = NHibernateManager.OpenSession();
            var criteria = session.CreateCriteria(typeof(T));

            if (!string.IsNullOrWhiteSpace(orderByProperty))
                criteria.AddOrder(Order.Asc(orderByProperty));
            else
                criteria.AddOrder(Order.Asc("Id"));

            if (maxResults.HasValue)
                criteria.SetMaxResults(maxResults.Value);

            return await criteria.ListAsync<T>();
        }

        public async Task<IList<T>> FindAllByProperty(string property, object value, string orderByProperty = null, int? maxResults = null)
        {
            using var session = NHibernateManager.OpenSession();
            var criteria = session.CreateCriteria(typeof(T));

            if (!string.IsNullOrWhiteSpace(orderByProperty))
                criteria.AddOrder(Order.Asc(orderByProperty));
            else
                criteria.AddOrder(Order.Asc("Id"));

            criteria.Add(value == null || value.ToString() == "null" ? Restrictions.IsNull(property) : Restrictions.Eq(property, value));

            if (maxResults.HasValue)
                criteria.SetMaxResults(maxResults.Value);

            return await criteria.ListAsync<T>();
        }

        public async Task<T> FindOneByProperty(string property, object value, string orderByProperty = null)
        {
            using var session = NHibernateManager.OpenSession();
            var criteria = session.CreateCriteria(typeof(T));

            if (!string.IsNullOrEmpty(orderByProperty))
                criteria.AddOrder(Order.Asc(orderByProperty));
            else
                criteria.AddOrder(Order.Asc("Id"));

            criteria.Add(value == null ? Restrictions.IsNull(property) : Restrictions.Eq(property, value));
            criteria.SetMaxResults(1);
            var result = await criteria.ListAsync<T>();

            if (result != null && result.Count > 0)
                return result[0];

            return default;
        }

        public async Task<IList<T>> FindAllByCriteria(ICriteria criteria)
        {
            return await criteria.ListAsync<T>();            
        }

        public async Task<IList<T>> FindAllByPropertyILike(string property, string value, string orderByProperty = null, int? maxResults = null)
        {
            using var session = NHibernateManager.OpenSession();
            var criteria = session.CreateCriteria(typeof(T));

            if (!string.IsNullOrEmpty(orderByProperty))
                criteria.AddOrder(Order.Asc(orderByProperty));
            else
                criteria.AddOrder(Order.Asc("Id"));

            criteria.Add(Restrictions.InsensitiveLike(property, value, MatchMode.Anywhere));

            if (maxResults.HasValue)
                criteria.SetMaxResults(maxResults.Value);

            return await criteria.ListAsync<T>();
        }        

        public async Task Create(T instance)
        {
            using var session = NHibernateManager.OpenSession();
            session.BeginTransaction();
            await session.SaveAsync(instance);
            // TODO: Save audit log
            session.Transaction.Commit();
            session.Close();
        }

        public async Task Update(T instance)
        {
            using var session = NHibernateManager.OpenSession();
            session.BeginTransaction();
            await session.UpdateAsync(instance);
            // TODO: Save audit log
            session.Transaction.Commit();
            session.Close();
        }

        public async Task Delete(T instance)
        {
            using var session = NHibernateManager.OpenSession();
            session.BeginTransaction();
            await session.DeleteAsync(instance);
            // TODO: Save audit log
            session.Transaction.Commit();
            session.Close();
        }
    }
}