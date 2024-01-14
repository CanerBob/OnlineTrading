namespace Trading.Service.Layer.Abstract;
public interface ICategoryService:IValidator<Category>
{
    Category GetById(int id);
	Task<Category> GetByIdAsync(int id);
	Task<List<Category>> GetAllAsync();
    List<Category> GetAll();
    Task<Category> CreateAsync(Category category);
    void Create(Category entity);
    void Update(Category entity);
    void Delete(Category entity);
    Category GetByIdWithProducts(int id);
	void DeleteFromCategory(int pid, int cid);
}