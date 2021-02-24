using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.DTOs;
using Entitites.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        ICategoryService _categoryService;

        public ProductManager(IProductDal productDal,ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
        }

        //attribute
        [ValidationAspect(typeof(ProductValidator))]
        public IResult Add(Product product)
        {

            //business codes

           IResult result = BusinessRules.Run(CheckIfProductNameExists(product.ProductName),
                CheckIfProductCountOfCategoryCorrect(product.CategoryId), CheckIfCategoryLimitExceded());
                
            if(result != null)
            {
                return result;
            }

            _productDal.Add(product);
             return new SuccessResult(Messages.ProductAdded);
           
               

         
            //business codes(iş kuralı: örneğin bir kişiye ehliyet veerip verrmemek için kontrol edilmesi olayı,veya bankada kredi verirken kişinin o krediye uygunnn olup olmadığı ,başvuru yapan kişinin finansal durumuna bakmak iş kodu denir.)
            //validation(ismin minumum 2 kaarakter olması,şifrenin şuna uyması vs gibi durumlara validation denir)

            //validation kodunu çalıştırma
            /*
            var context = new ValidationContext<Product>(product);
            ProductValidator productValidator = new ProductValidator();
            var result = productValidator.Validate(context); ;
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }*/

           // ValidationTool.Validate(new ProductValidator(), product);

            
       
        }

        public IDataResult<List<Product>> GetAll()
        {
            //İş kodları
            //Yetkisi var mı?

           // if (DateTime.Now.Hour == 23) {

             //   return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            //}

            return new SuccessDataResult<List<Product>>(_productDal.GetAll(),Messages.ProductListed);
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>( _productDal.GetAll(p => p.CategoryId == id));
        }

        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p=> p.ProductId == productId));
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
        }

        [ValidationAspect(typeof(ProductValidator))]
        public IResult Update(Product product)
        {
            throw new NotImplementedException();
        }

        /*    IResult IProductService.Add(Product product)
            {
                throw new NotImplementedException();
            }*/

        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
        {
            //Bir kategoride en fazla 10 ürün olabilir.
            var result = _productDal.GetAll(p => p.CategoryId == categoryId).Count;
            if (result >= 10)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }

            return new SuccessResult();
        }

        //aynı isimde ürün eklenemez

        private IResult CheckIfProductNameExists(string productName)
        {
            var result = _productDal.GetAll(p => p.ProductName == productName).Any();
            if (result == true)
            {
                return new ErrorResult(Messages.ProductNameAlredyExists);
            }

            return new SuccessResult();
        }

        //Eğer mevcut kategor sayısı 15 i geçtiyse sisteme yeni ürürn eklenemez

        private IResult CheckIfCategoryLimitExceded()
        {
            var result = _categoryService.GetAll();
            if (result.Data.Count > 15)
            {
                return new ErrorResult(Messages.CategoryLimitExceded);
            }
            return new SuccessResult();
        }

    }

   
}
