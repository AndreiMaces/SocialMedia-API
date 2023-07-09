using SocialMedia.Data;
using SocialMedia.Models;
using SocialMedia.Repository.IRepository;

namespace SocialMedia.Repository;

public class ReactionRepository: Repository<Reaction>, IReactionRepository
{
    private readonly ApplicationDbContext _db;

    public ReactionRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }
    
    public async Task<Reaction> UpdateAsync(Reaction entity)
    {
        entity.UpdatedDate = DateTime.Now;
        _db.Reactions.Update(entity);
        await _db.SaveChangesAsync();
        return entity;
    }
}