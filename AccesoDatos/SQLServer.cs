using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    public class SQLServer
    {
        private readonly string _connectionString;

        public SQLServer(string connectionString)
        {
            _connectionString = connectionString;
        }

        public T Scalar<T>(string query, SqlParameter[]? parameters = null)
        {
            try
            {
                using(SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using(SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        try
                        {
                            conn.Open();
                            if (parameters != null)
                            {
                                cmd.Parameters.AddRange(parameters);
                            }
                            return (T)cmd.ExecuteScalar();
                        }
                        catch (Exception)
                        {

                            throw;
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<T>? ScalarAsync<T>(string query, SqlParameter[]? parameters = null)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        try
                        {
                            await conn.OpenAsync();
                            if (parameters != null)
                            {
                                cmd.Parameters.AddRange(parameters);
                            }
                            return (T)await cmd.ExecuteScalarAsync();
                        }
                        catch (Exception)
                        {

                            throw;
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public T? Reader<T>(string query, SqlParameter[]? parameters = null) where T : class, new()
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.Text;

                        if (parameters != null)
                        {
                            sqlCommand.Parameters.AddRange(parameters);
                        }

                        sqlConnection.Open();

                        using (SqlDataReader reader = sqlCommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                T result = new T();
                                int count = reader.FieldCount - 1;
                                for (int i = 0; i <= count; i++)
                                {
                                    string columna = reader.GetName(i);
                                    PropertyInfo propiedad = result.GetType()?.GetProperty(columna)!;

                                    if (propiedad != null && !reader.IsDBNull(i))
                                    {
                                        propiedad.SetValue(result, reader.GetValue(i));
                                    }
                                }
                                return result;
                            }
                            return null;
                        }

                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<T>? ReaderList<T>(string query, SqlParameter[]? parameters = null) where T : new()
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection) { CommandType = CommandType.Text })
                    {
                        if (parameters != null)
                        {
                            sqlCommand.Parameters.AddRange(parameters);
                        }

                        sqlConnection.Open();

                        using (SqlDataReader reader = sqlCommand.ExecuteReader())
                        {
                            List<T> lista = new List<T>();

                            while (reader.Read())
                            {
                                T objeto = new T();

                                string[] nombreColumnas = reader.GetSchemaTable()?.Rows
                                    .Cast<DataRow>()?.Select(row => row["ColumnName"].ToString())?.ToArray()!;

                                var schemaTable = reader.GetSchemaTable();

                                foreach (string nombreColumna in nombreColumnas)
                                {
                                    PropertyInfo propiedad = objeto.GetType()?.GetProperty(nombreColumna)!;

                                    if (propiedad != null && !reader.IsDBNull(reader.GetOrdinal(nombreColumna)))
                                    {
                                        object valor = reader.GetValue(reader.GetOrdinal(nombreColumna));
                                        propiedad.SetValue(objeto, valor);
                                    }
                                }

                                lista.Add(objeto);
                            }
                            return lista;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void NonQuery(string query, SqlParameter[]? parameters = null)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection) { CommandType = CommandType.Text })
                    {
                        try
                        {
                            sqlConnection.Open();

                            if (parameters != null)
                            {
                                sqlCommand.Parameters.AddRange(parameters);
                            }

                            if (sqlCommand.ExecuteNonQuery() == 0)
                            {
                                throw new Exception("no se afectaron registros");
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
