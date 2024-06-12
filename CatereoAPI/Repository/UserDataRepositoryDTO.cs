using CatereoAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace CatereoAPI.Repository
{
    public class UserDataRepositoryDTO : IUserDataRepositoryDTO
    {
        private readonly ApplicationDBContext _context;

        public UserDataRepositoryDTO(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<int> GetIdByUserId(string userId)
        {
            var user = await _context.UserDataDTO.FirstOrDefaultAsync(u => u.UserId == userId);

            return user?.Id ?? 0; // Zwróć 0, jeśli nie znaleziono użytkownika
        }
    }
}
