using Library.DTO;
using Library.Models;

namespace Library.DAL
{
    public interface IAccountRepository : IDisposable
    {
        List<Account> GetAccounts();

        int? GetAccountId(string username);

        AccountStatus? GetAccountStatus(Login login);

        int Login(Login login);

        void InsertAccount(Account account);

        void UpdateAccount(Account account);

        void DeleteAccount(int id);

        void Save();
    }
}