namespace CatereoAPI.Repository
{
    public interface IUserDataRepositoryDTO
    {
        Task<int> GetIdByUserId(string userId);
    }
}
