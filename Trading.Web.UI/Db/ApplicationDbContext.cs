namespace Trading.Web.UI.Db;
public class ApplicationDbContext:IdentityDbContext<Person>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {}
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlServer("Data Source=CANER\\SQLEXPRESS;Integrated Security=True;Database=OnlineTradignDb;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;MultipleActiveResultSets=true");
		base.OnConfiguring(optionsBuilder);
	}
}