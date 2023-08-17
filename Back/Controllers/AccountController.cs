using Back.Utilities;
using Library.DAL;
using Library.DTO;
using Library.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Back.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IAccountRepository _account;
        private readonly IAdminRepository _admin;
        private readonly ICustomerRepository _customer;
        private readonly IUserRepository _user;

        public AccountController(CoffeehouseSystemContext context, IConfiguration configuration)
        {
            _account = new AccountRepository(context);
            _admin = new AdminRepository(context);
            _customer = new CustomerRepository(context);
            _user = new UserRepository(context);
            _configuration = configuration;
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
                    if (accountStatus.IsBanned == false)
                    {
                        Admin? isAdmin = _admin.GetAdminByAccountId(accountStatus.AccountId);
                        User? isUser = _user.GetUserByAccountId(accountStatus.AccountId);
                        Customer? isCustomer = _customer.GetCustomerByAccountId(accountStatus.AccountId);

                        string role = "";
                        string name = "";
                        string phone = "";
                        string email = "";
                        string address = "";
                        int profileId = -1;

                        // Prioritize higher privilege role

                        if (isCustomer != null)
                        {
                            role = "Customer";
                            name = isCustomer.Name;
                            phone = isCustomer.Phone;
                            email = isCustomer.Email;
                            address = isCustomer.Address;
                            profileId = isCustomer.CustomerId;
                        }

                        if (isUser != null)
                        {
                            role = "User";
                            name = isUser.CoffeeShopName;
                            phone = isUser.Phone;
                            email = isUser.Email;
                            address = isUser.Address;
                            profileId = isUser.UserId;
                        }

                        if (isAdmin != null)
                        {
                            role = "Admin";
                            name = isAdmin.Name;
                            phone = isAdmin.Phone;
                            email = isAdmin.Email;
                            address = isAdmin.Address;
                            profileId = isAdmin.AdminId;
                        }

                        var token = GenerateJwtToken(new TokenInfo
                        {
                            Id = accountStatus.AccountId,
                            ProfileId = profileId,
                            Role = role,
                            Name = name,
                            Phone = phone,
                            Email = email,
                            Address = address
                        });

                        return Ok(new
                        {
                            Token = token
                        });
                    }
                    else
                    {
                        return Unauthorized("Account is banned!");
                    }
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
            Customer tempCustomer;
            User tempUser;
            int? accountId = null;

            tempCustomer = _customer.GetCustomerByEmail(mail.Email);

            tempUser = _user.GetUserByEmail(mail.Email);

            if (tempCustomer != null || tempUser != null)
            {
                if (tempCustomer != null && tempUser != null) return BadRequest("User somehow has 2 different profiles.");

                if (tempCustomer != null) accountId = tempCustomer.AccountId;
                if (tempUser != null) accountId = tempUser.AccountId;
                if (accountId == null) return NotFound("Could not find the given account.");

                //Generate verification code
                string verificationCode = StringUtils.GenerateRandomString(10);
                _account.AddForgetCode((int)accountId, verificationCode);
                _account.Save();

                //Send mail
                EmailUtils email = new(_configuration);
                EmailDetails details = new()
                {
                    Subject = "Forget Password",
                    Body = $"Your verification code is: {verificationCode} \nPlease don't share it with anyone else.",
                };
                email.SendEmail(mail, details);

                return Ok(accountId);
            }

            return NotFound("Could not find the given account.");
        }

        [HttpPost]
        public IActionResult Verify(ForgetVerification verification)
        {
            if (string.IsNullOrEmpty(verification.VerificationCode)) return BadRequest("Empty verification code.");

            bool correctCode = _account.CheckForgetCode(verification.AccountId, verification.VerificationCode);

            if (correctCode)
            {
                _account.RemoveForgetCode(verification.AccountId);
                return Ok(verification.AccountId);
            }

            return BadRequest("Wrong verification code");
        }

        [HttpPatch]
        public IActionResult ResetPassword(ResetPassword reset)
        {
            if (string.IsNullOrEmpty(reset.Password)) return BadRequest("Password field is required!");

            try
            {
                _account.UpdateAccountPassword(reset.AccountId, reset.Password);
                _account.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return BadRequest();
        }

        //[Authorize(Roles = "Admin")]
        [HttpPatch("{id}")]
        public IActionResult UpdateBan(int id)
        {
            try
            {
                _account.UpdateBanStatus(id);
                _account.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return BadRequest();
        }

        private string GenerateJwtToken(TokenInfo tokenInfo)
        {
            var claims = new List<Claim>
            {
                new Claim(type: "id", tokenInfo.Id.ToString()),
                new Claim(type: "profileId", tokenInfo.ProfileId.ToString()),
                new Claim(ClaimTypes.Role, tokenInfo.Role),
                new Claim(type: "name", tokenInfo.Name),
                new Claim(type: "phone", tokenInfo.Phone),
                new Claim(ClaimTypes.Email, tokenInfo.Email),
                new Claim(type: "address", tokenInfo.Address),
                new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Aud, _configuration["Jwt:Audience"]),
                new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Iss, _configuration["Jwt:Issuer"])
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7), // Set token expiration time
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        protected override void Dispose(bool disposing)
        {
            _account.Dispose();
            _customer.Dispose();
            _user.Dispose();
            base.Dispose(disposing);
        }
    }
}