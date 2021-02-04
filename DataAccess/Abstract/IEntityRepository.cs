using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DataAccess.Abstract
{
    public interface IEntityRepository<T>
    {

        //ürünleri kategoriye göre listele vs işlemlerini yapabilmek için (filtreleme işlemi)
        List<T> GetAll(Expression<Func<T, bool>> filter=null);
        T Get(Expression<Func<T,bool>> filter );

        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);

       
    }
}
