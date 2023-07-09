using SocialMedia.Models;

namespace SocialMedia.Repository.IRepository;

public interface IReactionRepository: IRepository<Reaction>
{
    Task<Reaction> UpdateAsync(Reaction entity);
}