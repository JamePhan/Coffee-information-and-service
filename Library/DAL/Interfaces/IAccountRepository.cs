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

        void AddForgetCode(int accountId, string code);

        bool CheckForgetCode(int accountId, string code);

        void RemoveForgetCode(int accountId);

        void UpdateBanStatus(BanInfo banInfo);

        void InsertAccount(Account account);

        void UpdateAccount(Account account);

        void UpdateAccountPassword(int id, string password);

        void DeleteAccount(int id);

        void Save();
    }
}