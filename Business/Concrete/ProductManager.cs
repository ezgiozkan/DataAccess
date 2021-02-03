using Business.Abstract;
using DataAccess.Abstract;
using Entitites.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        public List<Product> GetAll()
        {
            return _productDal.GetAll();
        }

        public List<Product> GetAllByCategory(int id)
        {
            return _productDal.GetAll(p => p.CategoryId == id);
        }

        public List<Product> GeByUnitPrice(decimal min, decimal max)
        {
            return _productDal.GetAll(p=> p.UnitPrice>=min && p.UnitPrice<= max);
        }

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }
    }
}
