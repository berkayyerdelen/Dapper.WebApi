using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Dapper.WebApi.Services
{
    public abstract class BaseRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;


        protected BaseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        // use for buffered queries that return a type
        protected async Task<T> WithConnection<T>(Func<IDbConnection, Task<T>> getData)
        {
            try
            {
                await using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    return await getData(connection);
                }
            }
            catch (TimeoutException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL timeout", GetType().FullName),
                    ex);
            }
            catch (SqlException ex)
            {
                throw new Exception(
                    String.Format("{0}.WithConnection() experienced a SQL exception (not a timeout)",
                        GetType().FullName), ex);
            }
        }

        protected async Task WithConnection(Func<IDbConnection, Task> getData)
        {
            try
            {
                await using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    await getData(connection);
                }
            }
            catch (TimeoutException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL timeout", GetType().FullName),
                    ex);
            }
            catch (SqlException ex)
            {
                throw new Exception(
                    String.Format("{0}.WithConnection() experienced a SQL exception (not a timeout)",
                        GetType().FullName), ex);
            }
        }

        protected async Task<TResult> WithConnection<TRead, TResult>(Func<IDbConnection, Task<TRead>> getData,
            Func<TRead, Task<TResult>> process)
        {
            try
            {
                await using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var data = await getData(connection);
                    return await process(data);
                }
            }
            catch (TimeoutException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL timeout", GetType().FullName),
                    ex);
            }
            catch (SqlException ex)
            {
                throw new Exception(
                    String.Format("{0}.WithConnection() experienced a SQL exception (not a timeout)",
                        GetType().FullName), ex);
            }
        }
    }
}
