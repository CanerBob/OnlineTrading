namespace Trading.Service.Layer.Abstract;
public interface IProductService:IValidator<Product>
{
	Product GetProductDetails(string url);
    List<Product> GetSearchResult(string word);
    Product GetById(int id);
	Task<Product> GetByIdAsync(int id);
	List<Product> GetProductsByCategory(string name,int page, int pageSize);
    int GetCountByCategory(string category);
	List<Product> GetHomePageProducts();
    Task<List<Product>> GetAllAsync();
	List<Product> GetAll();
    Task<Product> CreateAsync(Product product);
    bool Create(Product entity);
    void Update(Product entity);
    bool Update(Product entity, int[] categoryIds);
    void Delete(Product entity);
    Product GetByIdWithCategories(int id);
}