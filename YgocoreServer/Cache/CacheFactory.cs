﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolServer.Cache
{
    class CacheFactory
    {
        public readonly static IAccountCache accountCache;

        static CacheFactory()
        {
            accountCache = new AccountCache();
        }
    }
}
