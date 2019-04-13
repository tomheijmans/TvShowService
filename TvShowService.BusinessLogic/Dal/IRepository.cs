using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace TvShowService.BusinessLogic.Dal
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IList<TEntity> Get(Expression<Func<TEntity, bool>> predicate = null, int skip = -1, int take = -1, string includeProperties = "");

        void Add(TEntity entity);

        void Update(TEntity entity);
    }
}