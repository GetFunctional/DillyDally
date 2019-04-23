﻿using System.Collections.Generic;
using System.Threading.Tasks;
using GF.DillyDally.Data.Contracts.Entities;

namespace GF.DillyDally.ReadModel.Deprecated.Common
{
    public interface ICommonDataRepository
    {
        Task<IList<ICurrencyEntity>> GetAllCurrencies();
    }
}