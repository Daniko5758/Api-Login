using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public interface IAspUser
    {
        IEnumerable<AspUser> GetAllUsers();
        AspUser RegisterUser(AspUser user);
        AspUser GetUserByUsername(string username);
        AspUser UpdateUser(AspUser user);
        void DeleteUser(string username);
        bool login(string username, string password);
    }
}