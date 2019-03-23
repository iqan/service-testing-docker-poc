using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DummyService.AcceptanceTests
{
    public class ConfigurationReader
    {
        private static IConfigurationRoot _configuration;

        static ConfigurationReader()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSettings.json")
                .AddEnvironmentVariables();

            _configuration = builder.Build();
        }

        public static string GetConnectionString(string name)
        {
            return _configuration.GetConnectionString(name);
        }

        public static string GetConfigValueFor(string key)
        {
            return _configuration.GetValue<string>(key);
        }
    }
}
