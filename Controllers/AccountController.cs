namespace EnsekMeterReadingsService.Controllers
{
    public class AccountController
    {

        public async Task<List<string>> GetAccountsAsync(DataContext dataContext)
        {
            List<Account> accounts = await dataContext.Accounts.ToListAsync();
            List<string> accountIdList = new List<string>();
            foreach(Account account in accounts)
            {
                accountIdList.Add(account.AccountId.ToString());
            }
            return accountIdList;
        }
    }
}


