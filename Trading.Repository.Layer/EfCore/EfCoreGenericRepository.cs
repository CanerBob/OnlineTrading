namespace Trading.Repository.Layer.EfCore;
public class EfCoreGenericRepository<TEntity, TContext> : IGenericRepository<TEntity> where TEntity : class
	where TContext : DbContext, new()
{
	public void Create(TEntity entity)
	{
		using (var context = new TContext())
		{
			context.Set<TEntity>().Add(entity);
			context.SaveChanges();
		}
	}

	public async Task<TEntity> CreateAsync(TEntity entity)
	{
		using (var context = new TContext())
		{
			await context.Set<TEntity>().AddAsync(entity);
			await context.SaveChangesAsync();
			return entity;
		}
	}

	public void Delete(TEntity entity)
	{
		using (var context = new TContext())
		{
			context.Set<TEntity>().Remove(entity);
			context.SaveChanges();
		}
	}
	public List<TEntity> GetAll()
	{
		using (var context = new TContext())
		{
			return context.Set<TEntity>().ToList();
		}
	}
	public async Task<List<TEntity>> GetAllAsync()
	{
		using (var context = new TContext())
		{
			return await context.Set<TEntity>().ToListAsync();
		}
	}
	public TEntity GetById(int id)
	{
		using (var context = new TContext())
		{
			return context.Set<TEntity>().Find(id);
		}
	}

	public async Task<TEntity> GetByIdAsync(int id)
	{
		using (var context = new AppDbContext())
		{
			return await context.Set<TEntity>().FindAsync(id);
		};
	}

	public virtual void Update(TEntity entity)
	{
		using (var context = new TContext())
		{
			context.Entry(entity).State = EntityState.Modified;
			context.SaveChanges();
		}
	}


}