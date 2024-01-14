namespace Trading.Repository.Layer.Abstarct;
public interface IProductRepository:IGenericRepository<Product>
{
    List<Product> GetSearchResult(string word);
    List<Product> GetHomePageProducts();
    int GetCountByCategory(string category);
    Product GetProductDetails(string url);
    List<Product> GetProductsByCategory(string name,int page,int pageSize);
	Product GetByIdWithCategories(int id);
	void Update(Product entity, int[] categoryIds);
}