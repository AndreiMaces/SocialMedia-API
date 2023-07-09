using SocialMedia.Models;

namespace SocialMedia.Repository.IRepository;

public interface ICommentRepository : IRepository<Comment>
{
    Task<Comment> UpdateAsync(Comment entity);
}