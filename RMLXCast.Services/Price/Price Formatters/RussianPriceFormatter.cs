using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMLXCast.Services.Price.Price_Formatters
{
    public class RussianPriceFormatter : IPriceFormatter
    {
        public string GetFormattedPrice(decimal price)
        {
            return $"{price} ₽";
        }
    }
}
