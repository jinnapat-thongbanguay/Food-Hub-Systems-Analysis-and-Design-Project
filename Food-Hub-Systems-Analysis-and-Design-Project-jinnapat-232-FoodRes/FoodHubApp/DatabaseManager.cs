using System;
using System.Data;
using Npgsql; // ต้อง Install NuGet Npgsql เวอร์ชัน 4.1.12 ก่อน

namespace FoodHubApp
{
    public class DatabaseManager
    {
        private string connString = "Host=localhost;Port=5432;Username=postgres;Password=112547;Database=FoodHubDB";

        // ฟังก์ชันอ่านข้อมูล (ดึงจาก Table Restaurants)
        public DataTable ExecuteQuery(string sql, NpgsqlParameter[] parameters = null)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    if (parameters != null) cmd.Parameters.AddRange(parameters);
                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    return dt;
                }
            }
        }

        // ฟังก์ชันเขียนข้อมูล (บันทึกลง Table Bookings)
        public bool ExecuteNonQuery(string sql, NpgsqlParameter[] parameters)
        {
            try
            {
                using (var conn = new NpgsqlConnection(connString))
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddRange(parameters);
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch { return false; }
        }
    }
}