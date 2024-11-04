using API.Models.Models;

namespace WebAPI.Data.Repository.IRepository
{
    public interface ITokenService
    {
        string CreateToken(AppUser appuser);
    }
}
