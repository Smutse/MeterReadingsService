namespace EnsekMeterReadingsService.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<MeterReadingUpload> MeterReadingUploads { get; set; }

        public DbSet<Account> Accounts { get; set; }

        public List<MeterReadingUpload> GetReadingUploads(DataContext dataContext)
        {
            return dataContext.MeterReadingUploads.ToList();
        }
    }
}
