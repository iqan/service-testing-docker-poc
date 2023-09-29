using DummyService.App.Infrastructure.Storage.Entities;
using Microsoft.EntityFrameworkCore;

namespace DummyService.App.Infrastructure.Storage
{
    public class DummyDbContext : DbContext
    {
        public DummyDbContext(DbContextOptions options) : base (options)
        {

        }

        public DbSet<MessageData> MessageDatas { get; set; }
    }
}
