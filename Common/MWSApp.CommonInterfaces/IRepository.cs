



using MWSApp.CommonModels.Entities;
using System.Linq.Expressions;

namespace MWSApp.CommonInterfaces
{
    public interface IRepository<T> where T : MainBase
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate);

        IQueryable<T> Get(Expression<Func<T, bool>> predicate);

        Task<IEnumerable<T>> GetListAsync(IQueryable<T> query);

        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
                                        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                        string includeString = null,
                                        bool disableTracking = true);
        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
                                       Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                       List<Expression<Func<T, object>>> includes = null,
                                       bool disableTracking = true);
        Task<T> GetByIdAsync(Guid id);
        Task<T> GetByIdIntAsync(int id);
        Task<T> AddAsync(T entity);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> objects);
        void Update(T entity);
        void UpdateState(T entity);
        void Delete(T entity);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<List<E>> GetListSqlAsync<E>(string query);
    }
}
