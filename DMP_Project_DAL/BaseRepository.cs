using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DMP_Project_Models;

namespace DMP_Project_DAL
{
    public abstract class BaseRepository
    {
        protected string ConnectionString { get; }                  // protected dus enkel degenen die overerven

        public BaseRepository()
        {
            ConnectionString = DatabaseConnection.Connectionstring("ComedyDB");
        }
    }
}
