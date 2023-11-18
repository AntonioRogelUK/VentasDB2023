using AccesoDatos;
using LogicaNegocios.Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocios.Commands
{
    public class ClienteCommand
    {
        private readonly string _connectionString;

        public ClienteCommand(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Cliente? ObtenerClientePorNombre(string nombre)
        {
            try
            {
                StringBuilder query = new StringBuilder();
                query.Append("SELECT");
                query.Append(" [Id]");
                query.Append(",[Codigo]");
                query.Append(",[Nombre]");
                query.Append(" FROM [Clientes]");
                query.Append(" WHERE [Nombre] LIKE '%' + @Nombre + '%'");

                SqlParameter[] parameters = {
                    new SqlParameter("@Nombre", nombre)
                };

                SQLServer sql = new SQLServer(_connectionString);
                Cliente? cliente = sql.Reader<Cliente>(query.ToString(), parameters);
                return cliente;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Cliente>? ObtenerClientesPorNombre(string nombre)
        {
            try
            {
                StringBuilder query = new StringBuilder();
                query.Append("SELECT");
                query.Append(" [Id]");
                query.Append(",[Codigo]");
                query.Append(",[Nombre]");
                query.Append(" FROM [Clientes]");
                query.Append(" WHERE [Nombre] LIKE '%' + @Nombre + '%'");

                SqlParameter[] parameters = {
                    new SqlParameter("@Nombre", nombre)
                };

                SQLServer sql = new SQLServer(_connectionString);
                var clientes = sql.ReaderList<Cliente>(query.ToString(), parameters);
                return clientes;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<int> ObtenerCantidadDeClientesAsync()
        {
            try
            {
                StringBuilder query = new StringBuilder();
                query.Append("SELECT COUNT(*)");
                query.Append(" FROM [Clientes]");

                SQLServer sql = new SQLServer(_connectionString);
                var clientes =await sql.ScalarAsync<int>(query.ToString())!;
                return clientes;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
