using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Collections.Generic;
using System.Data;
using FoodHubAPI.Models;

[ApiController]
[Route("api/[controller]")]
public class BookingsController : ControllerBase
{
    private readonly string connString;

    public BookingsController(IConfiguration configuration)
    {
        // connString = configuration.GetConnectionString("DefaultConnection");
        connString = "Host=localhost;Username=foodhub_admin;Password=AdminPass123;Database=FoodHubDB";
    }

    // 1. GET: api/bookings
    // เปลี่ยน resId เป็น int? เพื่อป้องกันคนพิมพ์ตัวอักษรเข้ามา
    [HttpGet]
    public IActionResult GetBookings([FromQuery] int? resId = null)
    {
        string sql = @"SELECT bookingid AS ""Booking ID"", 
                              customerid AS ""Customer ID"", 
                              restaurantid AS ""Restaurant ID"", 
                              bookingdate AS ""เวลาที่จอง"", 
                              status AS ""สถานะ""
                       FROM bookings";

        var parameters = new List<NpgsqlParameter>();

        // ตรวจสอบว่ามีการส่ง resId มาหรือไม่
        if (resId.HasValue)
        {
            // ใช้ Parameterized Query ป้องกัน SQL Injection
            sql += " WHERE restaurantid = @resId";
            parameters.Add(new NpgsqlParameter("resId", resId.Value));
        }

        sql += " ORDER BY bookingdate DESC";

        return Ok(ExecuteQueryFromDb(sql, parameters.ToArray()));
    }

    // 2. PUT: api/bookings/{id}/status 
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

    // ปรับปรุงฟังก์ชันนี้ให้รองรับการส่ง Parameters เข้าไปใน SQL ด้วย
    private List<Dictionary<string, object>> ExecuteQueryFromDb(string sql, NpgsqlParameter[] parameters = null)
    {
        var rows = new List<Dictionary<string, object>>();
        using (var conn = new NpgsqlConnection(connString))
        {
            conn.Open();
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                // ถ้ามี Parameter ให้เพิ่มเข้าไปใน Command
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }

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
        }
        return rows;
    }
}