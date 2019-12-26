using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kingdee.CAPP.Solidworks.RoutingPlugIn
{
    public class DbConfig
    {
        //private static string _connection = "server=10.50.110.32;database=PLM;uid=sa;pwd=sa";
        private static string _connection = "Data Source=HSZC1005-2080\\ADMINISTRATOR;Initial Catalog=PLM;User ID=sa;Password=123456;";
        public DbConfig(string connection)
        {
            _connection = connection;
        }
        public static string Connection
        {
            get
            {
                return _connection;
            }
            set
            {
                _connection = value;
            }
        }
    }
}
