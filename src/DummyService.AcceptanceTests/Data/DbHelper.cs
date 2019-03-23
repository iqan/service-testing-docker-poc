using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DummyService.AcceptanceTests.Data
{
    public class DbHelper
    {
        public static string ConnectionString => ConfigurationReader.GetConnectionString("DummyDatabase");

        public static DbContextOptions GetDbContextOptions()
        {
            if (string.IsNullOrEmpty(ConnectionString))
                throw new Exception("Please set connection string for database");
            var optionsBuilder = new DbContextOptionsBuilder();
            var options = optionsBuilder.UseSqlServer(ConnectionString).Options;
            return options;
        }
    }
}
