using Core.Interfaces;
using Infrastructure.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public  class FinRepository: IFinRepository
    {
        private readonly FinManagementEntities _context;

        public FinRepository(FinManagementEntities context)
        {
            _context = context;
        }
        public async Task<IEnumerable< User>>GetUser()
        {
            return await _context.Users.ToListAsync();
        }
    }
}
