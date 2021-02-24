using Entitites.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    //AbstrackValidator FluentValidatordan geliyor
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.ProductName).NotEmpty();
            RuleFor(p => p.ProductName).MinimumLength(2);
            RuleFor(p => p.UnitPrice).NotEmpty();
            //unitprice 0 dan büyük olmalı
            RuleFor(p => p.UnitPrice).GreaterThan(0);
            //categoryid 1 olduğu zaman unitprice 10  veya 10' a eşit olsun
            RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(10).When(p => p.CategoryId == 1);
            //Ürün ismi A ile başlamalı
            RuleFor(p => p.ProductName).Must(StartWithA).WithMessage("Ürünler A harfi ile başlamalı");

                

        }

        //true döndürüse kurala uygun değilse yanllış
        private bool StartWithA(string arg)
        {
            return arg.StartsWith("A");
        }
    }
}
