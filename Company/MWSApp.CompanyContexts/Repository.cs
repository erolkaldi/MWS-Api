﻿

using Microsoft.Extensions.Options;
using MWSApp.CommonModels.Models;

namespace MWSApp.CompanyContexts
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected CompanyDbContext _dbContext;
        //private readonly IBus _bus;
        private readonly RabbitMQSetting _rabbitMQSetting;
        public Repository(CompanyDbContext dbContext,IOptions<RabbitMQSetting> rabbitMQSetting)
        {
            _dbContext = dbContext;
            //_bus = bus;
            _rabbitMQSetting = rabbitMQSetting.Value;
        }

        public async Task<T> AddAsync(T entity)
        {
            entity.CreateDate = DateTime.Now;
            //TODO : CreateUser claimsden gelecek
            entity.CreateUser = "";
            await _dbContext.Set<T>().AddAsync(entity);
            return entity;
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }


        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeString = null, bool disableTracking = true)
        {
            IQueryable<T> query = _dbContext.Set<T>();
            if (disableTracking) query = query.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(includeString)) query = query.Include(includeString);

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
                return await orderBy(query).ToListAsync();
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<Expression<Func<T, object>>> includes = null, bool disableTracking = true)
        {
            IQueryable<T> query = _dbContext.Set<T>();
            if (disableTracking) query = query.AsNoTracking();

            if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
                return await orderBy(query).ToListAsync();
            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<T> GetByIdIntAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>().Where(predicate);
        }

        public void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.Update(entity).Property(x => x.Id).IsModified = false;
        }
        public void UpdateState(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }
        public async Task<IEnumerable<T>> GetListAsync(IQueryable<T> query)
        {
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> objects)
        {
            await _dbContext.Set<T>().AddRangeAsync(objects);
            return objects;
        }

        

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                try
                {
                    var updatedEntities = _dbContext.ChangeTracker.Entries().Where(x => x.State == EntityState.Modified);//&& x.Entity.GetType().Name == name);
                    var now = System.DateTime.UtcNow;
                    foreach (var entity in updatedEntities)
                    {
                        var idProperty= entity.OriginalValues.Properties.Where(x => x.Name == "Id").FirstOrDefault();
                        Guid id = Guid.Parse(entity.OriginalValues[idProperty].ToString());
                        foreach (var property in entity.OriginalValues.Properties)
                        {

                            var originalValue = entity.OriginalValues[property].ToString();
                            var updatedValue = entity.CurrentValues[property].ToString();
                            if (originalValue != updatedValue)
                            {
                                //TODO : User ve CompanyId claimsden gelecek
                                LogQueueMessage log = new LogQueueMessage();
                                log.OldValue = originalValue;
                                log.NewValue = updatedValue;
                                log.FieldName = property.Name;
                                log.TableName = entity.Entity.GetType().Name;
                                log.ActionDate = now;
                                log.ProjectName = "Company";
                                log.UserName = "";
                                log.TableId = id;
                                log.CompanyId = Guid.Empty;
                                //Uri uri = new Uri(_rabbitMQSetting.RabbitUri);
                                //var endPoint = await _bus.GetSendEndpoint(uri);
                                //endPoint.Send(log);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                }
                return await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<List<E>> GetListSqlAsync<E>(string query) 
        {
            return _dbContext.SQLQuery<E>(query);
        }

        #region IDisposable Members   
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }

            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}