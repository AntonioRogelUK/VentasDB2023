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

        public async Task<int?> ObtenerCantidadDeClientesAsync()
        {
            string connectionString =
                    "Server=localhost;" +
                    "Database=VentasDB;" +
                    "Trusted_Connection=True;" +
                    "TrustServerCertificate=true;";
            int? cantidadClientes = await new ClienteCommand(connectionString)
                .ObtenerCantidadDeClientesAsync();
            if (cantidadClientes == null)
            {
                throw new Exception("ERROR");//return "Cliente no encontrado";
            }
            return cantidadClientes;
        }

        public List<Cliente> ObtenerClientesPorNombre(string nombre)
        {
            string connectionString =
                    "Server=localhost;" +
                    "Database=VentasDB;" +
                    "Trusted_Connection=True;" +
                    "TrustServerCertificate=true;";
            var clientes = new ClienteCommand(connectionString)
                .ObtenerClientesPorNombre(nombre);

            /*List<string> clientesResult = new List<string>();

            foreach (Cliente cliente in clientes)
            {
                if (clientes != null)
                {
                    clientesResult.Add($"Cliente: {cliente.Id} {cliente.Nombre}");
                }
            }
            */
            return clientes;
        }
    }
}
