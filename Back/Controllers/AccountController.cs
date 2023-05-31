using Microsoft.AspNetCore.Mvc;
using Library.DAL;
using MimeKit;
using MailKit.Net.Smtp;
using Library.Models;
using Library.DTO;
using Back.Utilities;

namespace Back.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly IAccountRepository _account;
        private readonly ICustomerRepository _customer;
        private readonly IUserRepository _user;

        public AccountController(CoffeehouseSystemContext context)
        {
            _account = new AccountRepository(context);
            _customer = new CustomerRepository(context);
            _user = new UserRepository(context);
        }

        [HttpPost]
        public IActionResult Login(Login login)
        {
            int result = _account.Login(login);
            if (result == 0)
            {
                AccountStatus? accountStatus = _account.GetAccountStatus(login);
                if (accountStatus != null)
                {
                    return Ok(accountStatus);
                }
                else
                {
                    return BadRequest("Something weird happened, check the database");
                }
            }

            return BadRequest(result);
        }

        [HttpPost]
        public IActionResult Register(Register registerInfo)
        {
            if (_account.GetAccountId(registerInfo.Username) != null)
            {
                return BadRequest("Account already existed!");
            }

            Account newAccount = new()
            {
                Username = registerInfo.Username,
                Password = registerInfo.Password,
                IsBanned = false,
            };

            try
            {
                _account.InsertAccount(newAccount);
                _account.Save();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

            int? newlyAdded = _account.GetAccountId(registerInfo.Username);

            if (newlyAdded == null)
            {
                return BadRequest();
            }

            Customer newCustomer = new()
            {
                Name = registerInfo.Fullname,
                Phone = registerInfo.Phone,
                Address = "",
                Email = registerInfo.Email,
                AccountId = newlyAdded.Value,
            };

            try
            {
                _customer.InsertCustomer(newCustomer);
                _customer.Save();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

            return Ok();
        }

        [HttpPost]
        public IActionResult Forget(EmailEncapsulation mail)
        {
            Customer tempCustomer = null;
            User tempUser = null;

            tempCustomer = _customer.GetCustomerByEmail(mail.Email);

            tempUser = _user.GetUserByEmail(mail.Email);

            if (tempCustomer != null || tempUser != null)
            {
                //Generate verification code

                //Send mail
                MimeMessage email = new();
                email.From.Add(MailboxAddress.Parse("coffeehousesystem@gmail.com"));
                email.To.Add(MailboxAddress.Parse(mail.Email));
                email.Subject = "Testing";
                email.Body = new TextPart(MimeKit.Text.TextFormat.Plain)
                {
                    Text = "Email sent from api successfully!"
                };

                using SmtpClient smtp = new();
                smtp.Connect("smtp.gmail.com", 587, false);
                smtp.Authenticate("coffeehousesystem@gmail.com", "bwuoloxifjlacvzx");
                smtp.Send(email);
                smtp.Disconnect(true);

                return Ok();
            }

            return NotFound();
        }
    }
}