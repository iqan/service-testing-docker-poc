using DummyService.AcceptanceTests.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DummyService.AcceptanceTests.Data
{
    public class DummyDbContext : DbContext
    {
        public DummyDbContext(DbContextOptions options) : base (options)
        {

        }

        public DbSet<MessageData> MessageDatas { get; set; }
    }
}
