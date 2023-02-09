using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sights.Models;
using sqlite.Models;

namespace sights
{
    public sealed class AppConfig
    {
        private static readonly object instanceLock = new();

        private static AppConfig _instance = null;
        private static IConfigurationRoot _configuration;        

#if DEBUG
        private string _appsettingfile = "appsettings.Development.json";
#else
        private string _appsettingfile = "appsettings.json";
#endif
        private AppConfig()
        {
            var builder = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile(_appsettingfile, optional: true, reloadOnChange: true)
                                .AddUserSecrets("3d2b8454-7957-4457-9167-d64aaaedb8d3"); //Shared on one developer machine

            _configuration = builder.Build();
        }

        public static IConfigurationRoot ConfigurationRoot
        {
            get
            {
                lock (instanceLock)
                {
                    if (_instance == null)
                    {
                        _instance = new AppConfig();
                    }
                    return _configuration;
                }
            }
        }

        public static string CurrentDbType => ConfigurationRoot.GetValue<string>("CurrentDbType");
        public static string CurrentDbConnection => ConfigurationRoot.GetValue<string>("CurrentDbConnection");
        public static string CurrentDbConnectionString => ConfigurationRoot.GetConnectionString(CurrentDbConnection);

        public static List<User> GetUsers
        {
            get
            {
                var _users = new List<User>();
                ConfigurationRoot.Bind("Users", _users);
                return _users;
            }
        }
        public static List<User> GetJwtSetting
        {
            get
            {
                var _users = new List<User>();
                ConfigurationRoot.Bind("Users", _users);
                return _users;
            }
        }
    }
}
