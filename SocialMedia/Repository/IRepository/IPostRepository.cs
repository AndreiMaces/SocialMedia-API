using SocialMedia.Models;

namespace SocialMedia.Repository.IRepository;

public interface IPostRepository : IRepository<Post>
{
    Task<Post> UpdateAsync(Post entity);
    Task<List<Reaction>> GetReactionsAsync(int postId);
}