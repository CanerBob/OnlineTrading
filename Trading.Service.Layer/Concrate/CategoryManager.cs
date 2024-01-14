namespace Trading.Service.Layer.Concrate;
public class CategoryManager : ICategoryService
{
    private ICategoryRepository _categoryRepository;
    public CategoryManager(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }
	public void Create(Category entity)
    {
        _categoryRepository.Create(entity);
    }
    public void Delete(Category entity)
    {
        _categoryRepository.Delete(entity);
    }
	public void DeleteFromCategory(int pid, int cid)
	{
		_categoryRepository.DeleteFromCategory(pid, cid);
	}
    public List<Category> GetAll()
    {
        return _categoryRepository.GetAll();
    }
    public Category GetById(int id)
    {
        return _categoryRepository.GetById(id);
    }
	public Category GetByIdWithProducts(int id)
	{
		return _categoryRepository.GetByIdWithProducts(id);
	}
	public void Update(Category entity)
    {
        _categoryRepository.Update(entity);
    }
	public string ErrorMessage { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
	public bool Validation(Category entity)
	{
		throw new NotImplementedException();
	}

	public async Task<List<Category>> GetAllAsync()
	{
      return await  _categoryRepository.GetAllAsync();
	}
	public async Task<Category> GetByIdAsync(int id)
	{
		return await _categoryRepository.GetByIdAsync(id);
	}
	public async Task<Category> CreateAsync(Category category)
	{
		return await _categoryRepository.CreateAsync(category);
	}
}