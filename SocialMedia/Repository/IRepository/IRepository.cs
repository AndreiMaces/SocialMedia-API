using System.Linq.Expressions;

namespace SocialMedia.Repository.IRepository;

public interface IRepository<T> where T : class
{
    Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, string includeProprieties = null, int pageSize = 0, int pageNumber = 1);
    Task<T> GetAsync(Expression<Func<T, bool>> filter = null, bool tracked = true, string includeProprieties = null);
    Task CreateAsync(T entity);
    Task RemoveAsync(T entity);
    Task SaveAsync();

}