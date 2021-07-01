using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageService.API.Consts
{
    public enum ActionTypes : byte
    {
        READ = 1,
        DELETED = 2,
        FAVORITED = 3
    }
}

