using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CatereoAPI.Data;
using CatereoAPI.Model;

namespace CatereoAPI.Repository
{
    public class UsersRepository: UserRepositoryInterface
    {

        private readonly ApplicationDBContext _context;

        public UsersRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<ApplicationUser>> GetUsersAsync()
        {
            using (_context)
            {
                return await _context.Users.ToListAsync();
            }
        }
    }
}
