using FoodHubAPI.Models;
using Microsoft.AspNetCore.Mvc;

using Npgsql;

using System.Data;



[ApiController]

[Route("api/[controller]")]

public class PromotionsController : ControllerBase

{

    private readonly string connString = "Host=localhost;Username=foodhub_admin;Password=AdminPass123;Database=FoodHubDB";



    // 1. GET: api/promotions (โหลดโปรโมชั่นทั้งหมด)

    [HttpGet]

    public IActionResult GetAllPromotions()

    {

        string sql = @"SELECT p.promotionid AS ""ID"", r.name AS ""ชื่อร้าน"", 

                       p.name AS ""ชื่อโปรโมชั่น"", p.discountamount AS ""ส่วนลด"", p.status AS ""สถานะ""

                       FROM Promotions p

                       JOIN Restaurants r ON p.restaurantid = r.restaurantid

                       ORDER BY p.promotionid ASC";

        return Ok(ExecuteQueryFromDb(sql));

    }



    // 2. GET: api/promotions/search/{resId} (ค้นหาตาม ID ร้านอาหาร)

    [HttpGet("search/{resId}")]

    public IActionResult SearchByRestaurant(int resId)

    {

        string sql = $@"SELECT p.promotionid AS ""ID"", r.name AS ""ชื่อร้าน"", 

                       p.name AS ""ชื่อโปรโมชั่น"", p.discountamount AS ""ส่วนลด"", p.status AS ""สถานะ""

                       FROM Promotions p

                       JOIN Restaurants r ON p.restaurantid = r.restaurantid

                       WHERE p.restaurantid = {resId}";

        return Ok(ExecuteQueryFromDb(sql));

    }



    // 3. PUT: api/promotions/{id} (แก้ไขโปรโมชั่น)

    [HttpPut("{id}")]

    public IActionResult UpdatePromotion(int id, [FromBody] PromotionUpdateDto dto)

    {

        string sql = @"UPDATE Promotions 

                       SET name = @name, 

                           discountamount = @discount, 

                           status = @status 

                       WHERE promotionid = @id";



        using (var conn = new NpgsqlConnection(connString))

        {

            conn.Open();

            using (var cmd = new NpgsqlCommand(sql, conn))

            {

                cmd.Parameters.AddWithValue("name", dto.Name);

                cmd.Parameters.AddWithValue("discount", dto.DiscountAmount);

                cmd.Parameters.AddWithValue("status", dto.Status);

                cmd.Parameters.AddWithValue("id", id);



                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0) return Ok(new { message = "แก้ไขสำเร็จ" });

                return NotFound(new { message = "ไม่พบ ID โปรโมชั่นที่ระบุ" });

            }

        }

    }



    // 4. DELETE: api/promotions/{id} (ลบโปรโมชั่น)

    [HttpDelete("{id}")]

    public IActionResult DeletePromotion(int id)

    {

        string sql = "DELETE FROM Promotions WHERE promotionid = @id";

        using (var conn = new NpgsqlConnection(connString))

        {

            conn.Open();

            using (var cmd = new NpgsqlCommand(sql, conn))

            {

                cmd.Parameters.AddWithValue("id", id);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0) return Ok(new { message = "ลบสำเร็จ" });

                return NotFound(new { message = "ไม่พบ ID โปรโมชั่น" });

            }

        }

    }



    // ฟังก์ชันช่วยดึงตารางออกมาเป็น List ของ Dictionary เพื่อปล่อยออกไปเป็น JSON

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