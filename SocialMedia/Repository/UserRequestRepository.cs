using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Data;
using SocialMedia.Models;
using SocialMedia.Repository.IRepository;

namespace SocialMedia.Repository;

public class UserRequestRepository: Repository<UserRequest>, IUserRequestRepository
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;
    internal DbSet<UserRequest> _dbSet;
    
    public UserRequestRepository(ApplicationDbContext db, IMapper mapper) : base(db)
    {
        _db = db;
        _mapper = mapper;
        _dbSet = _db.Set<UserRequest>();
    }
}