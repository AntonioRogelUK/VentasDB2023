using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos;

namespace LogicaNegocios
{
    public class Class1
    {
        public string ObtenerFecha()
        {
            string connectionstring =
                    "Server=localhost;" +
                    "Database=PruebaDB;" +
                    "Trusted_Connection=True;" +
                    "TrustServerCertificate=true;";

            SQLServer sql = new SQLServer(connectionstring);

            //string fecha = sql.Scalar<string>("SELECT CONVERT(varchar,GETDATE())");
            string fecha = sql.Scalar<string>("SELECT [Name] FROM [Users] WHERE [Id]=1");
            return fecha;
        }
    }
}
