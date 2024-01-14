namespace Trading.Service.Layer.Concrate;
public class ProductManager : IProductService
{
    private IProductRepository _productRepository;
    public ProductManager(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
	public async Task<Product> CreateAsync(Product product)
	{
		return await _productRepository.CreateAsync(product);
	}
	public bool Create(Product entity)
    {
        if (Validation(entity)) 
        {
			_productRepository.Create(entity);
            return true;
		}
        return false;
    }
    public void Delete(Product entity)
    {
        _productRepository.Delete(entity);
    }
	public List<Product> GetAll()
    {
       return _productRepository.GetAll();
    }
    public Product GetById(int id)
    {
        return _productRepository.GetById(id);
    }
	public Product GetByIdWithCategories(int id)
	{
		return _productRepository.GetByIdWithCategories(id);
	}
	public int GetCountByCategory(string category)
    {
        return _productRepository.GetCountByCategory(category);
    }
	public List<Product> GetHomePageProducts()
	{
        return _productRepository.GetHomePageProducts();
	}
	public Product GetProductDetails(string url)
	{
		return _productRepository.GetProductDetails(url);
	}
	public List<Product> GetProductsByCategory(string name, int page, int pageSize)
	{
		return _productRepository.GetProductsByCategory(name,page,pageSize);
	}
    public List<Product> GetSearchResult(string word)
    {
        return _productRepository.GetSearchResult(word);
    }
    public void Update(Product entity)
    {
        _productRepository.Update(entity);
    }
	public bool Update(Product entity, int[] categoryIds)
	{
        if (Validation(entity)) 
        {
            if (categoryIds.Length == 0) 
            {
                ErrorMessage += "Ürün Güncelleme İçin En Az Bir Kategori Seçmelisiniz";
                return false;
            }
			_productRepository.Update(entity, categoryIds);
            return true;
		}
        return false;
	}
	public bool Validation(Product entity)
	{
        var isValid = true;
        if (string.IsNullOrEmpty(entity.Name)) 
        {
            ErrorMessage += "Ürün İsmi Girmelisiniz.\n";
            isValid= false;
        }
        if (entity.Price < 0) 
        {
            ErrorMessage += "Fiyat Alanı Negatif Olamaz.\n";
            isValid= false;
        }
        if (string.IsNullOrEmpty(entity.Description)) 
        {
            ErrorMessage = "Açıklama Girmelisiniz.\n";
            isValid= false;
        }
        if (string.IsNullOrEmpty(entity.Url)) 
        {
            ErrorMessage = "Url Alanı Gereklidir.\n";
            isValid= false;
        }
        return isValid;
	}

	public async Task<List<Product>> GetAllAsync()
	{
        return await _productRepository.GetAllAsync();
	}
	public async Task<Product> GetByIdAsync(int id)
	{
		return await _productRepository.GetByIdAsync(id);
	}
	public string ErrorMessage { get; set; }
}