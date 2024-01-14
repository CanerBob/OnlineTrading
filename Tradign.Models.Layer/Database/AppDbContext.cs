namespace Trading.Models.Layer.Database;
public class AppDbContext:DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; } 
    public DbSet<ProductCategory> ProductCategories { get; set; }
	public DbSet<Basket> Basket { get; set; }
	public DbSet<BasketItem> BasketItems { get; set; }
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
		optionsBuilder.UseSqlServer("Data Source=CANER\\SQLEXPRESS;Integrated Security=True;Database=OnlineTradignDb;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;MultipleActiveResultSets=true");
        base.OnConfiguring(optionsBuilder);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductCategory>()
            .HasKey(x => new { x.CategoryId, x.ProductId });
    }
}