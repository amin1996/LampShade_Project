﻿using _0_FrameWork.Domain;
using AccountManagement.Application.Contracts.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagement.Domain.AccountAgg
{
    public interface IAccountRepository:IRepository<long, Account>
    {
        Account GetBy(string username);
        EditAccount GetDetails(long id);
        List<AccountViewModel> Search(AccountSearchModel searchModel);
    }
}
