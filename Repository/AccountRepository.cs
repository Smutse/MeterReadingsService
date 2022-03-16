using EnsekMeterReadingsService.Interfaces;

namespace EnsekMeterReadingsService.Repository
{
    public class AccountRepository : iAccountRepository
    {
        private readonly DataContext _context;
        public AccountRepository(DataContext context)
        {
            _context = context;
        }
        public ICollection<Account> GetAccountIds()
        {
            return _context.Accounts.OrderBy(p => p.AccountId).ToList();
        }

        public ICollection<Account> GetAccounts()
        {
            return _context.Accounts.OrderBy(p => p.AccountId).ToList();
        }
    }
}
