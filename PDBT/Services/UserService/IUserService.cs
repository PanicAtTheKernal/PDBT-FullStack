using PDBT_CompleteStack.Models;

namespace PDBT_CompleteStack.Services.UserService;

public interface IUserService
{
    Task<ServiceResponse<User>> Register(UserRegistration registrationRequest);
    Task<ServiceResponse<RefreshTokenDTO>> Login(UserDTO loginRequest, HttpResponse httpResponse);
    Task<ServiceResponse<RefreshTokenDTO>> RefreshToken(HttpRequest httpRequest, HttpResponse httpResponse);
    Task<IEnumerable<User>> GetUsersByList(ICollection<int> ids, int projectId);
    Task<User?> GetUserById(int id);
}