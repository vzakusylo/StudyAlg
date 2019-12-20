﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Services
{
    public interface IIdentityService
    {
        string GetUserIdentity();
        string GetUserName();
    }
}
