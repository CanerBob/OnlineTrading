using Microsoft.EntityFrameworkCore;

namespace Trading.Repository.Layer.Abstract;
public interface IGenericRepository<T> where T : class
{
    T GetById(int id);
    Task<T> GetByIdAsync(int id);
    Task<List<T>> GetAllAsync();
	List<T> GetAll();
    void Create(T entity);
    Task<T> CreateAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
}