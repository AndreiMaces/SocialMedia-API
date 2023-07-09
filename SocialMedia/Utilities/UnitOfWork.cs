using SocialMedia.Repository.IRepository;
using SocialMedia.Utilities.IUtilities;

namespace SocialMedia.Utilities;

public class UnitOfWork : IUnitOfWork
{
    public IUserRepository Users { get; set; }
    public IPostRepository Posts { get; set; }
    public ICommentRepository Comments { get; set; }
    public IReactionRepository Reactions { get; set; }
    public IUserRequestRepository UserRequests { get; set; }
    
    public UnitOfWork(IUserRepository users, IPostRepository posts, ICommentRepository comments, IReactionRepository reactions, IUserRequestRepository userRequests)
    {
        Users = users;
        Posts = posts;
        Comments = comments;
        Reactions = reactions;
        UserRequests = userRequests;
    }
}