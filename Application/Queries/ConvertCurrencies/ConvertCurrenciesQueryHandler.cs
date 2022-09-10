using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Queries.ConvertCurrencies
{
    public class ConvertCurrenciesQueryHandler: IRequestHandler<ConvertCurrenciesQuery,double>
    {
        private readonly ICurrencyConverter _currencyConverter;

        public ConvertCurrenciesQueryHandler(ICurrencyConverter currencyConverter)
        {
            _currencyConverter = currencyConverter;
        }

        public async Task<double> Handle(ConvertCurrenciesQuery request, CancellationToken cancellationToken)
        {
            return await _currencyConverter.Convert(request.fromCurrency, request.toCurrency, request.amount);
        }
    }
}
