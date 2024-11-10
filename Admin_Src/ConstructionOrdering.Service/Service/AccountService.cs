using ConstructionOdering.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionOrdering.Service.Service
{
    public class AccountService
    {
        private readonly AdminDbConsturctionOderingSystemContext _context;

        public AccountService(AdminDbConsturctionOderingSystemContext context)
        {
            _context = context;
        }

        public bool VerifyPassword(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username);

            if(user == null)
            {
                return false;
            }

            return user.Password == password;
        }
    }
}
