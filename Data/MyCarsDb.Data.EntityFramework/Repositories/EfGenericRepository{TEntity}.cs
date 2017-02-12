namespace MyCarsDb.Data.EntityFramework.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using MyCarsDb.Data.Contracts.Repositories;
    using MyCarsDb.Data.EntityFramework.Contracts;
    using MyCarsDb.Data.Models.Contracts;    

    public abstract class EfGenericRepository<TEntity> : IDbGenericRepository<TEntity> 
        where TEntity : class, IEntity
    {
        private IMyCarsDbContext _dbContext;
        private DbSet<TEntity> _dbSet;

        public EfGenericRepository(IMyCarsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected DbSet<TEntity> DbSet
        {
            get
            {
                if (_dbSet == null)
                {
                    _dbSet = _dbContext.Set<TEntity>();
                }

                return _dbSet;
            }
        }

        protected IMyCarsDbContext DbContext
        {
            get
            {
                return _dbContext;
            }
        }

        public virtual void Add(TEntity entity)
        {
            this.DbSet.Add(entity);
        }

        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            this.DbSet.AddRange(entities);
        }


        public Task<TEntity> FindByIdAsync(object id)
        {
            return this.DbSet.FindAsync(id);
        }

        public virtual IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate)
        {
            return this.DbSet.Where(predicate).ToList();
        }

        public virtual async Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await this.DbSet.Where(predicate).ToListAsync();            
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return this.DbSet.ToList();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await this.DbSet.ToListAsync();
        }

        public virtual void Update(TEntity entity)
        {
            var entry = _dbContext.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.DbSet.Attach(entity);
            }

            entry.State = EntityState.Modified;
        }

        public virtual void UpdateRange(IEnumerable<TEntity> entities)
        {
            foreach (TEntity entity in entities)
            {
                Update(entity);
            }
        }


        public virtual void Remove(TEntity entity)
        {
            var entry = _dbContext.Entry(entity);
            if (entry.State != EntityState.Deleted)
            {
                entry.State = EntityState.Deleted;
            }
            else
            {
                this.DbSet.Attach(entity);
                this.DbSet.Remove(entity);
            }
        }

        public virtual void RemoveRange(IEnumerable<TEntity> entities)
        {
            this.DbSet.RemoveRange(entities);
        }
    }
}
