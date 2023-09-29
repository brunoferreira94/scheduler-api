using Microsoft.EntityFrameworkCore;
using Scheduler.Domain.Interfaces;
using Scheduler.Domain.Entities;
using Scheduler.Infra.Data.Context;
using System.Linq.Expressions;

namespace Scheduler.Infra.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        #region 'Properties'

        protected readonly SchedulerContext _context;

        protected DbSet<TEntity> DbSet
        {
            get
            {
                return _context.Set<TEntity>();
            }
        }

        #endregion 'Properties'

        public Repository(SchedulerContext context)
        {
            _context = context;
        }

        #region 'Create/Update/Delete/Save Methods'

        public TEntity Create(TEntity model)
        {
            try
            {
                DbSet.Add(model);
                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<TEntity> Create(IEnumerable<TEntity> models)
        {
            try
            {
                DbSet.AddRange(models);
                return models;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Update(TEntity model)
        {
            try
            {
                var entry = _context.Entry(model);

                DbSet.Attach(model);

                entry.State = EntityState.Modified;

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Update(IEnumerable<TEntity> models)
        {
            try
            {
                foreach (var registro in models)
                {
                    var entry = _context.Entry(registro);
                    DbSet.Attach(registro);
                    entry.State = EntityState.Modified;
                }

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Caso haja compatibilidade, onde TEntity herde de 'Entity',
        /// o campo 'IsDeleted' será definido para true automaticamente,
        /// utilizando a exclusão lógica, como uma das regras internas de sistemas.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Delete(TEntity model)
        {
            try
            {
                if (model is Entity)
                {
                    (model as Entity).IsDeleted = true;
                    var _entry = _context.Entry(model);

                    DbSet.Attach(model);

                    _entry.State = EntityState.Modified;
                }
                else
                {
                    var _entry = _context.Entry(model);
                    DbSet.Attach(model);
                    _entry.State = EntityState.Deleted;
                }

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Delete(Guid id)
        {
            try
            {
                var model = DbSet.Find(id);
                return (model != null) && Delete(model);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Delete(params object[] Keys)
        {
            try
            {
                var model = DbSet.Find(Keys);
                return (model != null) && Delete(model);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Delete(Expression<Func<TEntity, bool>> where)
        {
            try
            {
                var model = DbSet.Where<TEntity>(where).FirstOrDefault<TEntity>();

                return (model != null) && Delete(model);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Delete(List<TEntity> models)
        {
            try
            {
                foreach (var registro in models)
                {
                    Delete(registro);
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Executa instrucao SQL dentro de uma Transacao
        /// </summary>
        /// <param name="query"> Instrucao SQL a ser executada </param>
        /// <returns> Verdadeiro em caso de sucesso. </returns>
        public bool ExecuteSQL(string query)
        {
            try
            {
                _context.Database.BeginTransaction();

                _context.Database.ExecuteSqlRaw(query);

                _context.Database.CommitTransaction();

                return true;
            }
            catch (Exception)
            {
                _context.Database.RollbackTransaction();

                throw;
            }
        }

        #endregion 'Create/Update/Delete/Save Methods'

        #region 'Search Methods'

        public IQueryable<TEntity> QueryPagedAndSortDynamic<TType>(Expression<Func<TEntity, bool>> where, Func<IQueryable<TEntity>, object> includes, Expression<Func<TEntity, TType>> select, string sotyByPropertie, string containsProperty, int page, int itemsPerPage)
        {
            try
            {
                IQueryable<TEntity> _filtered = DbSet.Where(where);

                //_filtered = _filtered.OrderBy(sotyByPropertie);

                IQueryable<TType> _pagedProperty = _filtered.Select(select)
                    .Skip(page * itemsPerPage)
                    .Take(itemsPerPage);

                IQueryable<TEntity> _query = DbSet.Where(wh => _pagedProperty.Contains(EF.Property<TType>(wh, containsProperty)));

                if (includes != null)
                    _query = includes(_query) as IQueryable<TEntity>;

                return _query.Where(where);
                //.OrderBy(sotyByPropertie);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public TEntity Find(params object[] Keys)
        {
            try
            {
                return DbSet.Find(Keys);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public TEntity Find(Expression<Func<TEntity, bool>> where)
        {
            try
            {
                return DbSet.FirstOrDefault(where);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public TEntity Find(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, object> includes = null, bool tracking = false)
        {
            try
            {
                IQueryable<TEntity> _query = DbSet;

                if (includes != null)
                    _query = includes(_query) as IQueryable<TEntity>;

                if (tracking)
                    return _query.AsTracking().SingleOrDefault(predicate);

                return _query.SingleOrDefault(predicate);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IQueryable<TEntity> Query()
        {
            return DbSet;
        }

        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> where)
        {
            try
            {
                return DbSet.Where(where);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, object> includes)
        {
            try
            {
                IQueryable<TEntity> _query = DbSet;

                if (includes != null)
                    _query = includes(_query) as IQueryable<TEntity>;

                return _query.Where(predicate).AsQueryable();
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion 'Search Methods'

        #region 'Async Methods'

        #region 'Create/Update/Delete/Save Methods'

        public async Task<TEntity> CreateAsync(TEntity model)
        {
            try
            {
                DbSet.Add(model);
                await SaveAsync();
                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<TEntity>> CreateAsync(IEnumerable<TEntity> model)
        {
            try
            {
                DbSet.AddRange(model);
                await SaveAsync();
                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateAsync(TEntity model)
        {
            try
            {
                var entry = _context.Entry(model);

                DbSet.Attach(model);

                entry.State = EntityState.Modified;

                return await SaveAsync() > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteAsync(TEntity model)
        {
            try
            {
                var entry = _context.Entry(model);

                DbSet.Attach(model);

                entry.State = EntityState.Deleted;

                return await SaveAsync() > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteAsync(params object[] Keys)
        {
            try
            {
                var model = DbSet.Find(Keys);
                return (model != null) && await DeleteAsync(model);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> where)
        {
            try
            {
                var model = DbSet.FirstOrDefault(where);

                return (model != null) && await DeleteAsync(model);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> SaveAsync()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion 'Create/Update/Delete/Save Methods'

        #region 'Search Methods'

        public async Task<TEntity> GetAsync(params object[] Keys)
        {
            try
            {
                return await DbSet.FindAsync(Keys);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> where)
        {
            try
            {
                return await DbSet.AsNoTracking().FirstOrDefaultAsync(where);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion 'Search Methods'

        #endregion 'Async Methods'

        public void Dispose()
        {
            try
            {
                _context?.Dispose();
                GC.SuppressFinalize(this);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}