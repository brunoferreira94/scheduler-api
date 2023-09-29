using System.Linq.Expressions;

namespace Scheduler.Domain.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        #region 'Create/Update/Delete/Save Methods'

        TEntity Create(TEntity model);

        IEnumerable<TEntity> Create(IEnumerable<TEntity> models);

        bool Update(TEntity model);

        bool Update(IEnumerable<TEntity> model);

        bool Delete(TEntity model);

        bool Delete(params object[] Keys);

        bool Delete(Expression<Func<TEntity, bool>> where);

        bool Delete(List<TEntity> model);

        bool ExecuteSQL(string query);

        #endregion

        #region 'Search Methods'

        IQueryable<TEntity> QueryPagedAndSortDynamic<TType>(Expression<Func<TEntity, bool>> where, Func<IQueryable<TEntity>, object> includes, Expression<Func<TEntity, TType>> select, string sortyByPropertie, string containsProperty, int page, int itensPerPage);

        TEntity Find(params object[] Keys);

        TEntity Find(Expression<Func<TEntity, bool>> where);

        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> where);

        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, object> includes);

        #endregion

        #region 'Async Methods'

        #region 'Create/Update/Delete/Save Methods'

        Task<TEntity> CreateAsync(TEntity model);

        Task<IEnumerable<TEntity>> CreateAsync(IEnumerable<TEntity> model);

        Task<bool> UpdateAsync(TEntity model);

        Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> where);

        Task<bool> DeleteAsync(TEntity model);

        Task<bool> DeleteAsync(params object[] Keys);

        Task<int> SaveAsync();

        #endregion

        #region 'Search Methods'

        Task<TEntity> GetAsync(params object[] Keys);

        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> where);

        #endregion

        #endregion
    }
}
