namespace EnsekMeterReadingsService.Interfaces
{
    public interface iAccountRepository
    {
        ICollection<Account> GetAccounts();
    }
}
