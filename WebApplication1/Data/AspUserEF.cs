using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class AspUserEF : IAspUser
    {
        private readonly ApplicationDbContext _context;
        public AspUserEF(ApplicationDbContext context)
        {
            _context = context;
        }

        public void DeleteUser(string username)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AspUser> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public AspUser GetUserByUsername(string username)
        {
            return _context.AspUsers.FirstOrDefault(u => u.Username == username);
        }

        public bool login(string username, string password)
        {
            var user = _context.AspUsers.FirstOrDefault(u => u.Username == username);
            if (user == null) return false;

            return WebApplication1.Helper.HashHelper.VerifyPassword(password, user.Password);
        }

        public AspUser RegisterUser(AspUser user)
        {
            try
            {
                if (user == null)
                {
                    throw new ArgumentNullException(nameof(user), "User cannot be null");
                }
                user.Password = Helper.HashHelper.HashPassword(user.Password);
                _context.AspUsers.Add(user);
                _context.SaveChanges(); 
                return user;
            }
            catch (DbUpdateException dbex)
            {
                throw new Exception("An error occurred while registering the user.", dbex);
            }
            catch (System.Exception ex)
            {
                throw new Exception("An unexpected error occurred while registering the user.", ex);
            }
        }

        public AspUser UpdateUser(AspUser user)
        {   
            throw new NotImplementedException();
        }
    }
}