using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace BikeShop
{
    public class DBConnection
    {
        OracleConnection conn;

        public DBConnection()
        {
            try
            {
                string strCon = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=loonshot.cgxkzseoyswk.us-east-2.rds.amazonaws.com)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=ORCL)));User Id=loonshot;Password=loonshot123;";
                conn = new OracleConnection(strCon);
                conn.Open();

                WriteLine("BO Connection OK...");
                //MessageBox.Show("DB Connection OK...");
            }
            catch (Exception err)
            {
                //MessageBox.Show(err.ToString());
                WriteLine(err.ToString());
            }
        }

    }
    
    
}
