using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos;
using LogicaNegocios.Commands;
using LogicaNegocios.Entities;

namespace LogicaNegocios
{
    public class Class1
    {
        public string ObtenerClientePorNombre(string nombre)
        {
            string connectionString =
                    "Server=localhost;" +
                    "Database=VentasDB;" +
                    "Trusted_Connection=True;" +
                    "TrustServerCertificate=true;";
            Cliente? cliente = new ClienteCommand(connectionString)
                .ObtenerClientePorNombre(nombre);
            if (cliente == null)
            {
                return "Cliente no encontrado";
            }
            return $"Cliente: {cliente.Id} {cliente.Nombre}";
        }
    }
}
