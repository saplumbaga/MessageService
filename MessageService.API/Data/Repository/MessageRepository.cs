﻿using MessageService.API.Data;
using MessageService.API.Data.Entities;
using MessageService.API.Data.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageService.API.Data.Repository
{
    public class MessageRepository : RepositoryBase<Message>
    {
        private readonly MessageServiceDBContext _context;
        public MessageRepository(MessageServiceDBContext context) : base(context)
        {
            _context = context;
        }
    }
}
