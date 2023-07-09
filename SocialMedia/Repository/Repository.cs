using Microsoft.EntityFrameworkCore;
using SocialMedia.Data;
using SocialMedia.Repository.IRepository;
using System.Linq.Expressions;

namespace SocialMedia.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly ApplicationDbContext _db;
    internal DbSet<T> dbSet;

    public Repository(ApplicationDbContext db)
    {
        _db = db;
        this.dbSet = _db.Set<T>();
    }
    public async Task CreateAsync(T entity)
    {
        await dbSet.AddAsync(entity);
        await SaveAsync();
    }

    public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, string includeProprieties = null, int pageSize = 0, int pageNumber = 1)
    {
        IQueryable<T> query = dbSet;
        if (filter != null)
            query = query.Where(filter);
        if (includeProprieties != null)
            foreach (var includeProp in includeProprieties.Split(',', StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProp);
        if (pageNumber > 0)
        {
            if (pageSize > 100)
                pageSize = 100;
            query = query.Skip(pageSize * (pageNumber - 1)).Take(pageSize);
        }
        return await query.ToListAsync();
    }

    public async Task<T> GetAsync(Expression<Func<T, bool>> filter = null, bool tracked = true, string includeProprieties = null)
    {
        IQueryable<T> query = dbSet;
        if (!tracked)
            query = query.AsNoTracking();
        if (filter != null)
            query = query.Where(filter);
        if (includeProprieties != null)
            foreach (var includeProp in includeProprieties.Split(',', StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProp);
        return await query.FirstOrDefaultAsync();
    }

    public async Task RemoveAsync(T entity)
    {
        dbSet.Remove(entity);
        await SaveAsync();
    }

    public async Task SaveAsync()
    {
        await _db.SaveChangesAsync();
    }
}