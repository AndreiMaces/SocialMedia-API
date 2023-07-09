using SocialMedia.Repository.IRepository;

namespace SocialMedia.Utilities.IUtilities;

public interface IUnitOfWork
{
    IUserRepository Users { get; set; }
    IPostRepository Posts { get; set; }
    ICommentRepository Comments { get; set; }
    IReactionRepository Reactions { get; set; }
    IUserRequestRepository UserRequests { get; set; }
}