﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Core.Repositories
{
    public interface ICashRepository : IDisposable
    {
        decimal GetAvailableMoneyAmount();
    }
}
