using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;


namespace Handler
{
    public static class DbHandler
    {
        private static string _connectionString = "Your_Connection_String_Here";

        /// <summary>
        /// Veritabanından tek bir kayıt çeker.
        /// </summary>
        /// <typeparam name="T">Dönen veri tipi</typeparam>
        /// <param name="query">SQL sorgusu</param>
        /// <param name="parameters">Sorgu parametreleri</param>
        /// <returns>Tek bir kayıt</returns>
        public static async Task<T> QuerySingleAsync<T>(string query, object parameters = null)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            return await connection.QuerySingleOrDefaultAsync<T>(query, parameters);
        }

        /// <summary>
        /// Veritabanından birden fazla kayıt çeker.
        /// </summary>
        /// <typeparam name="T">Dönen veri tipi</typeparam>
        /// <param name="query">SQL sorgusu</param>
        /// <param name="parameters">Sorgu parametreleri</param>
        /// <returns>Birden fazla kayıt</returns>
        public static async Task<IEnumerable<T>> QueryAsync<T>(string query, object parameters = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                return await connection.QueryAsync<T>(query, parameters);
            }
        }

        /// <summary>
        /// Veritabanına veri ekleme, güncelleme veya silme işlemleri yapar.
        /// </summary>
        /// <param name="query">SQL sorgusu</param>
        /// <param name="parameters">Sorgu parametreleri</param>
        /// <returns>İşlem sonucu etkilenen satır sayısı</returns>
        public static async Task<int> ExecuteAsync(string query, object parameters = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                return await connection.ExecuteAsync(query, parameters);
            }
        }

        /// <summary>
        /// Bir işlem (transaction) başlatır ve bu işlem içerisinde birden fazla sorgu çalıştırır.
        /// </summary>
        /// <param name="commands">Çalıştırılacak sorgular ve parametreler</param>
        /// <returns>İşlem sonucu</returns>
        public static async Task<bool> ExecuteTransactionAsync(IEnumerable<(string Query, object Parameters)> commands)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        foreach (var command in commands)
                        {
                            await connection.ExecuteAsync(command.Query, command.Parameters, transaction);
                        }
                        transaction.Commit();
                        return true;
                    }
                    catch
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }
    }
}
