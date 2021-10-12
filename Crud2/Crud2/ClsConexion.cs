using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;

namespace Crud2
{
    class ClsConexion
    {
        public static SqlConnection Conectar(){

            SqlConnection cn = new SqlConnection("SERVER=DESKTOP-5KIJC16;DATABASE=REGISTRO;integrated security=true;");
            cn.Open();
            return cn;
        
        }
        

        
    }
}
