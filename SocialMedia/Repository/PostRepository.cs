using Microsoft.EntityFrameworkCore;
using SocialMedia.Data;
using SocialMedia.Models;
using SocialMedia.Repository.IRepository;

namespace SocialMedia.Repository;

public class PostRepository : Repository<Post>, IPostRepository
{
    private readonly ApplicationDbContext _db;

    public PostRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }
    public async Task<Post> UpdateAsync(Post entity)
    {
        entity.UpdatedDate = DateTime.Now;
        _db.Posts.Update(entity);
        await _db.SaveChangesAsync();
        return entity;
    }
    public async Task<List<Reaction>> GetReactionsAsync(int postId)
    {
        return await _db.Reactions.Where(r => r.PostId == postId).ToListAsync();
    }
}