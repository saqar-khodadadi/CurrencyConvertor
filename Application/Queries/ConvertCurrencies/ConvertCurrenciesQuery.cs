using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Application.Queries.ConvertCurrencies
{
    public class ConvertCurrenciesQuery:IRequest<double>
    {
        public ConvertCurrenciesQuery(string fromCurrency, string toCurrency, double amount)
        {
            this.fromCurrency = fromCurrency;
            this.toCurrency = toCurrency;
            this.amount = amount;
        }

        public string fromCurrency { get; set; }
        public string toCurrency { get; set; }
        public double amount { get; set; }
    }
}
