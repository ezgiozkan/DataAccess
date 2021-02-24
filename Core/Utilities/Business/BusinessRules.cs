using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Business
{
   public class BusinessRules
    {

        //bütün kuralları gez
        public static IResult Run(params IResult[] logics)
        {
            foreach (var logic in logics)
            {
                //kurala uymayan varsa onu döndür
                if (!logic.Success)
                {
                    return logic;
                }
            }

            return null;
        }
    }
}
