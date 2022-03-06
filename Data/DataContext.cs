namespace EnsekMeterReadingsService.Data
{
    /// <summary>
    /// Data context for meter reading database
    /// </summary>
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
