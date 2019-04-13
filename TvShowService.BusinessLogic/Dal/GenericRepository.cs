using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace TvShowService.BusinessLogic.Dal
{
    public class GenericRepository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        private readonly TvShowDbContext context;
        private readonly DbSet<TEntity> dbSet;

        public GenericRepository(TvShowDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            dbSet = context.Set<TEntity>();
        }

        /// <summary>
        /// Add the given entity to the repository
        /// </summary>
        /// <param name="entity"></param>
        public void Add(TEntity entity)
        {
            dbSet.Add(entity);
        }

        /// <summary>
        /// Get entities from the repository using the given options
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public IList<TEntity> Get(Expression<Func<TEntity, bool>> predicate = null, int skip = -1, int take = -1, string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (skip > -1)
            {
                query = query.Skip(skip);
            }
            if (take > -1)
            {
                query = query.Take(take);
            }

            foreach (string includeProperty in includeProperties.Split
               (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return query.ToList();
        }

        /// <summary>
        /// Update the given entity
        /// </summary>
        /// <param name="entity"></param>
        public void Update(TEntity entity)
        {
            dbSet.Update(entity);
        }
    }
}
