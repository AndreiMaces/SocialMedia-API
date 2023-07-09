using SocialMedia.Models;
using SocialMedia.Models.DTOs;

namespace SocialMedia.Repository.IRepository;

public interface IUserRepository : IRepository<User>
{
    bool IsEmailUnique(string email);
    Task<AuthResponseDTO> Login(LoginRequestDTO loginRequestDTO);
    Task<AuthResponseDTO> Register(RegisterRequestDTO registerRequestDTO);
    Task<List<Post>> GetFeed(int id, int pageSize = 0, int pageNumber = 1);
    Task<List<User>> GetFriends(int id, int pageSize = 0, int pageNumber = 1);
    Task<List<UserRequest>> GetFriendRequests(int id, int pageSize = 0, int pageNumber = 1);
    Task<User> UpdateAsync(User entity);
}