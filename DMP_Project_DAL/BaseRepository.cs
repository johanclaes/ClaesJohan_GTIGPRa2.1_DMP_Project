using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace DMP_Project_DAL
{
    public abstract class BaseRepository
    {
        protected string ConnectionString { get; }

        public BaseRepository()
        {
            ConnectionString = DatabaseConnection.Connectionstring("Comedy");
        }
    }
}
