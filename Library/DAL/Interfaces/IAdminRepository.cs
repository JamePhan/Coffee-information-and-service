﻿using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL
{
    public interface IAdminRepository : IDisposable
    {
        Admin? GetAdminByAccountId(int accountId);
    }
}