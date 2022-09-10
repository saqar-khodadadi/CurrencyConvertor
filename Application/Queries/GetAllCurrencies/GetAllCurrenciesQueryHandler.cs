using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Domains;
using MediatR;

namespace Application.Queries.GetAllCurrencies
{
    public class GetAllCurrenciesQueryHandler:IRequestHandler<GetAllCurrenciesQuery, IEnumerable<Currency>>
    {
        private readonly ICurrencyRepository _currencyRepository;

        public GetAllCurrenciesQueryHandler(ICurrencyRepository currencyRepository)
        {
            _currencyRepository = currencyRepository;
        }

        public async Task<IEnumerable<Currency>> Handle(GetAllCurrenciesQuery request, CancellationToken cancellationToken)
        {
            return await _currencyRepository.GetAll();
        }
    }
}
