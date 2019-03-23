using DummyService.App.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DummyService.App.Infrastructure.Persistence
{
    public class DummyDbContext : DbContext
    {
        public DummyDbContext(DbContextOptions options) : base (options)
        {

        }

        public DbSet<MessageData> MessageDatas { get; set; }
    }
}
