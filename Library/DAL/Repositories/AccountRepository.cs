using Library.DTO;
using Library.Models;
using Microsoft.Data.SqlClient;

namespace Library.DAL
{
    public class AccountRepository : IAccountRepository
    {
        private readonly CoffeehouseSystemContext _context;
        private bool _disposed = false;

        public AccountRepository(CoffeehouseSystemContext context)
        {
            _context = context;
        }

        public List<Account> GetAccounts()
        {
            return _context.Accounts.ToList();
        }

        public int? GetAccountId(string username)
        {
            Account? account = _context.Accounts.FirstOrDefault(account => account.Username.Equals(username));
            if (account != null)
            {
                return account.AccountId;
            }
            return null;
        }

        public AccountStatus? GetAccountStatus(Login login)
        {
            Account? account = _context.Accounts.FirstOrDefault(account => account.Username.Equals(login.Username) && account.Password.Equals(login.Password));

            if (account != null)
            {
                return new AccountStatus
                {
                    AccountId = account.AccountId,
                    IsBanned = account.IsBanned,
                };
            }
            return null;
        }

        /// <summary>
        /// Return integer status to check login
        /// </summary>
        /// <param name="login">Login object, contains username and password</param>
        /// <returns>0: Account Exist, Correct Password;
        /// 1: Account Exist, Incorrect Password;
        /// 2: Account does not exist
        /// </returns>

        public int Login(Login login)
        {
            Account? checkExist = _context.Accounts.FirstOrDefault(account => account.Username.Equals(login.Username));

            if (checkExist != null)
            {
                return checkExist.Password!.Equals(login.Password) ? 0 : 1;
            }

            return 2;
        }

        public void AddForgetCode(int accountId, string code)
        {
            Account? checkExist = _context.Accounts.FirstOrDefault(account => account.AccountId == accountId);

            if (checkExist != null)
            {
                checkExist.ForgetCode = code;
                UpdateAccount(checkExist);
            }
            else
            {
                throw new Exception("No such account found!");
            }
        }

        public bool CheckForgetCode(int accountId, string code)
        {
            Account? checkExist = _context.Accounts.FirstOrDefault(account => account.AccountId == accountId);

            if (checkExist != null && !string.IsNullOrEmpty(checkExist.ForgetCode) && checkExist.ForgetCode.Equals(code))
            {
                return true;
            }

            return false;
        }

        public void RemoveForgetCode(int accountId)
        {
            Account? checkExist = _context.Accounts.FirstOrDefault(account => account.AccountId == accountId);

            if (checkExist != null)
            {
                checkExist.ForgetCode = "";
                UpdateAccount(checkExist);
            }
            else
            {
                throw new Exception("No such account found!");
            }
        }

        public void UpdateBanStatus(BanInfo banInfo)
        {
            int? accountId = null;

            if (banInfo.Role.ToLower().Equals("admin"))
                throw new Exception("Cannot ban an Administrator!");

            if (banInfo.Role.ToLower().Equals("customer"))

                accountId = _context.Customers.FirstOrDefault(cust => cust.CustomerId.Equals(banInfo.ProfileId)).AccountId;

            if (banInfo.Role.ToLower().Equals("user"))

                accountId = _context.Users.FirstOrDefault(user => user.UserId.Equals(banInfo.ProfileId)).AccountId;

            if (banInfo.Role.ToLower().Equals("customer"))

                accountId = _context.Customers.FirstOrDefault(cust => cust.CustomerId.Equals(banInfo.ProfileId)).AccountId;

            if (accountId == null) throw new Exception("No such account found");

            Account? checkExist = _context.Accounts.FirstOrDefault(acc => acc.AccountId.Equals(accountId));

            if (checkExist != null)
            {
                checkExist.IsBanned = !checkExist.IsBanned;
                UpdateAccount(checkExist);
            }
            else
            {
                throw new Exception("No such account found!");
            }
        }

        public void InsertAccount(Account account)
        {
            _context.Accounts.Add(account);
        }

        public void UpdateAccount(Account account)
        {
            _context.Entry(account).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public void UpdateAccountPassword(int id, string password)
        {
            Account? toUpdate = _context.Accounts.FirstOrDefault(account => account.AccountId == id);
            if (toUpdate != null)
            {
                toUpdate.Password = password;
                _context.Entry(toUpdate).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }
            else
            {
                throw new Exception("No such account found!");
            }
        }

        public void DeleteAccount(int id)
        {
            Account? toDelete = _context.Accounts.FirstOrDefault(account => account.AccountId == id);
            if (toDelete != null)
            {
                _context.Accounts.Remove(toDelete);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _context.Dispose();
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}