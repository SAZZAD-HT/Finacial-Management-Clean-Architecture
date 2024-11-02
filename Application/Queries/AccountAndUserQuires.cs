using Core.DTos;
using Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Queries
{
    public class AccountAndUserQuires : IRequest<List<UserDto>>
    {
        public int? AccountId { get; set; }
    }
public class AccountAndUserQuiresHandler : IRequestHandler<AccountAndUserQuires, List<UserDto>>
    {
        private readonly IFinRepository _finRepository;
        public AccountAndUserQuiresHandler(IFinRepository finRepository)
        {
            _finRepository = finRepository;
        }
        public async Task<List<UserDto>> Handle(AccountAndUserQuires request, CancellationToken cancellationToken)
        {
            try
            {
                IEnumerable<UserDto> data = await _finRepository.GetUser((int)request.AccountId);
                return data.ToList();
            }
            catch (Exception ex)
            {
                return new List<UserDto> { new UserDto { UserID = 0 } };
            }
        }
    }

}
