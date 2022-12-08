using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMP_Project_DAL
{
    public static class DatabaseConnection
    {
        internal static string Connectionstring(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

    }
}
