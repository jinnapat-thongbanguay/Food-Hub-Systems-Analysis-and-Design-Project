using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Data;
using FoodHubAPI.Models; // อ้างอิงไปยังโฟลเดอร์โมเดลของคุณ

[ApiController]
[Route("api/[controller]")]
public class BookingsController : ControllerBase
{
    private readonly string connString = "Host=localhost;Username=foodhub_admin;Password=AdminPass123;Database=FoodHubDB";

    // 1. GET: api/bookings (โหลดข้อมูลการจองทั้งหมด หรือกรองตาม Restaurant ID)
    [HttpGet]
    public IActionResult GetBookings([FromQuery] string resId = "")
    {
        string sql = @"SELECT bookingid AS ""Booking ID"", 
                              customerid AS ""Customer ID"", 
                              restaurantid AS ""Restaurant ID"", 
                              bookingdate AS ""เวลาที่จอง"", 
                              status AS ""สถานะ""
                       FROM bookings";

        if (!string.IsNullOrEmpty(resId))
        {
            sql += $" WHERE restaurantid = {resId}";
        }

        sql += " ORDER BY bookingdate DESC";

        return Ok(ExecuteQueryFromDb(sql));
    }

    // 2. PUT: api/bookings/{id}/status (อัปเดตสถานะการจอง)
    [HttpPut("{id}/status")]
    public IActionResult UpdateStatus(int id, [FromBody] BookingUpdateDto dto)
    {
        string sql = "UPDATE bookings SET status = @status WHERE bookingid = @id";

        using (var conn = new NpgsqlConnection(connString))
        {
            conn.Open();
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("status", dto.Status);
                cmd.Parameters.AddWithValue("id", id);

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    return Ok(new { message = "อัปเดตสถานะสำเร็จ" });
                }
                return NotFound(new { message = "ไม่พบรายการจองที่ระบุ" });
            }
        }
    }

    // ฟังก์ชันช่วยแปลงข้อมูลตารางออกมาเป็น JSON
    private List<Dictionary<string, object>> ExecuteQueryFromDb(string sql)
    {
        var rows = new List<Dictionary<string, object>>();
        using (var conn = new NpgsqlConnection(connString))
        {
            conn.Open();
            using (var cmd = new NpgsqlCommand(sql, conn))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var row = new Dictionary<string, object>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        row[reader.GetName(i)] = reader.GetValue(i);
                    }
                    rows.Add(row);
                }
            }
        }
        return rows;
    }
}