using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DTO
{
    public class ForgetVerification
    {
        public int AccountId { get; set; }
        public string VerificationCode { get; set; }
    }
}