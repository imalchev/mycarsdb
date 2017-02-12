namespace MyCarsDb.Data.Contracts.Repositories
{    
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Linq.Expressions;

    using MyCarsDb.Data.Models.Contracts;    

    public interface IDbGenericRepository<TEntity> 
        where TEntity : class, IEntity
    {
        IEnumerable<TEntity> GetAll();
        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity> FindByIdAsync(object id);
        IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        void Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}
