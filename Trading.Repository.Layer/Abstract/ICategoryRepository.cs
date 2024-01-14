namespace Trading.Repository.Layer.Abstract;
public interface ICategoryRepository:IGenericRepository<Category>
{
	Category GetByIdWithProducts(int id);
	void DeleteFromCategory(int pid, int cid);
}