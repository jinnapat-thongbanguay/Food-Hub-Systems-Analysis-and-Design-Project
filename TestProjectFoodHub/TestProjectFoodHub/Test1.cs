using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Data;
using System.Runtime.InteropServices;

[ApiController]
[Route("api/[controller]")]
public class FoodHubApiController : ControllerBase
{
    // ย้าย Connection String มาไว้ที่เซิร์ฟเวอร์ 
    private string connString = "Host=localhost;Username=foodhub_app;Password=AppPass123;Database=FoodHubDB";

    // 1. Endpoint สำหรับดึงรายชื่อร้านอาหารทั้งหมด
    // GET: api/foodhubapi/restaurants
    [HttpGet("restaurants")]
    public IActionResult GetRestaurants()
    {
        string sql = "SELECT restaurantid, name, address, phone FROM restaurants";
        return Ok(ExecuteQueryFromDb(sql)); // ส่งกลับเป็น JSON อัตโนมัติ
    }

    // 2. Endpoint สำหรับดูโปรโมชั่นที่ Active
    // GET: api/foodhubapi/promotions/active
    [HttpGet("promotions/active")]
    public IActionResult GetActivePromotions()
    {
        string sql = @"SELECT p.name AS ""ชื่อโปรโมชั่น"", r.name AS ""ร้านอาหาร"", 
                       p.discountamount AS ""ส่วนลด"", p.enddate AS ""วันหมดเขต""
                       FROM Promotions p
                       JOIN Restaurants r ON p.restaurantid = r.restaurantid
                       WHERE p.status = 'Active'";
        return Ok(ExecuteQueryFromDb(sql));
    }

    // 3. Endpoint สำหรับดูรายชื่อลูกค้าที่เช็คอิน
    // GET: api/foodhubapi/bookings/checked-in
    [HttpGet("bookings/checked-in")]
    public IActionResult GetCheckedInCustomers()
    {
        string sql = @"SELECT c.fullname AS ""ชื่อลูกค้า"", r.name AS ""ร้านอาหาร"", 
                       b.bookingdate AS ""เวลาจอง"", b.status AS ""สถานะ""
                       FROM Bookings b
                       JOIN Customers c ON b.customerid = c.customerid
                       JOIN Restaurants r ON b.restaurantid = r.restaurantid
                       WHERE b.status = 'CheckedIn'";
        return Ok(ExecuteQueryFromDb(sql));
    }

    // ฟังก์ชันช่วยรัน SQL ในฝั่งเซิร์ฟเวอร์
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