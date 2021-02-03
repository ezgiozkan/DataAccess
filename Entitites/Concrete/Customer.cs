using Entitites.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entitites.Concrete
{
   public class Customer: IEntity
    {
        public int CustomerId { get; set; }
        public int ContactName { get; set; }
        public int CompanyName { get; set; }
        public int City { get; set; }

    }
}
