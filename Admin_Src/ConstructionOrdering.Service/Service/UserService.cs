using ConstructionOdering.Repositories.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionOrdering.Service.Service
{
    public class UserService
    {
        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public string GetUserNameById(int userId)
        {
            return _userRepository.GetUsernameById(userId);
        }
    }
}
