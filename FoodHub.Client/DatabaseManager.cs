using Microsoft.IdentityModel.Protocols;
using Npgsql;
using System.Configuration;

public class DatabaseManager
{
    // ดึงค่าเชื่อมต่อจาก App.config
    private string connString = ConfigurationManager.ConnectionStrings["PostgreDb"].ConnectionString;

    public void TestConnection()
    {
        using (var conn = new NpgsqlConnection(connString))
        {
            conn.Open(); // ถ้าเปิดผ่านแสดงว่าเชื่อม pgAdmin ได้แล้ว
        }
    }

    // ฟังก์ชันสำหรับ "จองอาหาร/โปรโมชั่น" ตาม Flow ในรูป Mermaid
    public void CreateBooking(int customerId, int restaurantId, int promotionId, int peopleCount)
    {
        using (var conn = new NpgsqlConnection(connString))
        {
            conn.Open();
            string sql = "INSERT INTO Bookings (CustomerID, RestaurantID, PromotionID, NumberOfPeople, Status, CreatedAt) " +
                         "VALUES (@cid, @rid, @pid, @pcount, 'Confirmed', NOW())";

            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("cid", customerId);
                cmd.Parameters.AddWithValue("rid", restaurantId);
                cmd.Parameters.AddWithValue("pid", promotionId);
                cmd.Parameters.AddWithValue("pcount", peopleCount);
                cmd.ExecuteNonQuery();
            }
        }
    }
}