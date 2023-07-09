using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Data;
using SocialMedia.Models;
using SocialMedia.Models.DTOs;
using SocialMedia.Repository.IRepository;
using SocialMedia.Services.IServices;

namespace SocialMedia.Repository;

public class UserRepository : Repository<User>, IUserRepository
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;
    internal DbSet<User> _dbSet;
    private readonly IPasswordService _passwordService;
    private readonly IJWTService _jwtService;

    public UserRepository(ApplicationDbContext db, IMapper mapper, IPasswordService passwordService, IJWTService jwtService) : base(db)
    {
        _db = db;
        _mapper = mapper;
        _dbSet = _db.Set<User>();
        _passwordService = passwordService;
        _jwtService = jwtService;
    }
        
    public async Task<List<UserRequest>> GetFriendRequests(int id, int pageSize = 0, int pageNumber = 1)
    {
        if (pageNumber > 0)
        {
            if (pageSize > 100)
                pageSize = 100;
        }

        var user = await _db.Users
            .Include(u => u.Requests)
            .FirstOrDefaultAsync(u => u.Id == id);

        var friendRequests = user.Requests
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToList();

        return friendRequests;
    }

    public async Task<List<User>> GetFriends(int id, int pageSize = 0, int pageNumber = 1)
    {
        if (pageNumber > 0)
        {
            if (pageSize > 100)
                pageSize = 100;
        }

        var user = await _db.Users
            .Include(u => u.Friends)
            .FirstOrDefaultAsync(u => u.Id == id);

        var friends = user.Friends
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToList();

        return friends;
    }
        
    public async Task<List<Post>> GetFeed(int myId, int pageSize = 0, int pageNumber = 1)
    {
        if (pageNumber > 0)
        {
            if (pageSize > 100)
                pageSize = 100;
        }

        var user = await _db.Users
            .Include(u => u.Friends)
            .ThenInclude(f => f.Posts)
            .ThenInclude(p => p.Comments)
            .Include(u => u.Friends)
            .ThenInclude(f => f.Posts)
            .FirstOrDefaultAsync(u => u.Id == myId);

        var feed = user.Friends
            .SelectMany(f => f.Posts)
            .OrderByDescending(p => p.CreatedDate)
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToList();

        return feed;
    }


    public async Task<User> UpdateAsync(User entity)
    {
        entity.UpdatedDate = DateTime.Now;
        _db.Users.Update(entity);
        await _db.SaveChangesAsync();
        return entity;
    }


    public async Task<AuthResponseDTO> Login(LoginRequestDTO loginRequestDTO)
    {
        IQueryable<User> query = _dbSet;
        query = query.Where(u =>
            u.Email == loginRequestDTO.Email && u.Password == _passwordService.EncodePassword(loginRequestDTO.Password));
        foreach (var includeProp in "Requests,Friends,Posts".Split(',', StringSplitOptions.RemoveEmptyEntries))
            query = query.Include(includeProp);

        var user = await query.FirstOrDefaultAsync();
        if (user == null)
        {
            return new AuthResponseDTO()
            {
                Token = "",
                User = null
            };
        }

        var token = _jwtService.GenerateJWT(user);
        var loginResponseDTO = new AuthResponseDTO()
        {
            Token = token,
            User = _mapper.Map<UserDTO>(user)
        };
        return loginResponseDTO;
    }

    public async Task<AuthResponseDTO> Register(RegisterRequestDTO registerRequestDTO)
    {
        var user = new User()
        {
            Email = registerRequestDTO.Email,
            Name = registerRequestDTO.Name,
            Password = _passwordService.EncodePassword(registerRequestDTO.Password),
            Friends = new List<User>(),
            Posts = new List<Post>(),
            Requests = new List<UserRequest>()
        };
        _db.Users.Add(user);
        await _db.SaveChangesAsync();
        var token = _jwtService.GenerateJWT(user);
        var loginResponseDTO = new AuthResponseDTO()
        {
            Token = token,
            User = _mapper.Map<UserDTO>(user)
        };
        return loginResponseDTO;
    }

    public bool IsEmailUnique(string email)
    {
        var user = _db.Users.FirstOrDefault(u => u.Email == email);
        if (user == null) return true;
        return false;
    }
}