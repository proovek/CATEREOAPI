using CatereoAPI.Data;
using CatereoAPI.Model;

namespace CatereoAPI.Repository
{
    public interface UserRepositoryInterface
    {
        Task<List<ApplicationUser>> GetUsersAsync();
    }

}
