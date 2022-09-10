using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Domains;
using MediatR;

namespace Application.Queries.GetAllCurrencies
{
    public class GetAllCurrenciesQuery:IRequest<IEnumerable<Currency>>
    {

    }
}
