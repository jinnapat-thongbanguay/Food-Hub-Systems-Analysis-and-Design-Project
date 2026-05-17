using Microsoft.AspNetCore.Mvc;
using Npgsql; // สำหรับเชื่อมต่อ PostgreSQL
using System.Collections.Generic;
using System;

namespace FoodHubAPI.Controllers
{
    // 1. กำหนดว่าเป็น API และตั้งค่า Route (URL)
    [ApiController]
    [Route("api/foodhubapi")]
    public class FoodHubApiController : ControllerBase // 2. เปลี่ยนจาก Controller เป็น ControllerBase
    {
        // connString 
        private readonly string connString = "Host=localhost;Username=foodhub_admin;Password=AdminPass123;Database=FoodHubDB";

        // 3. สร้าง Endpoint สำหรับดึงข้อมูลร้านอาหาร
        [HttpGet("restaurants")]
        public IActionResult GetRestaurants()
        {
            try
            {
                string sql = "SELECT restaurantid, name, address, phone FROM restaurants";
                return Ok(ExecuteQueryFromDb(sql)); // ส่งกลับเป็น JSON
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message }); // ถ้า Error ให้โชว์สาเหตุ
            }
        }

        // --- ส่วนที่ต้องเพิ่ม: Endpoint สำหรับดึงข้อมูลโปรโมชันที่ Active ---
        [HttpGet("promotions/Active")]
        public IActionResult GetActivePromotions()
        {
            try
            {
                // ⚠️ หมายเหตุ: ลองเช็คชื่อคอลัมน์และตารางใน pgAdmin อีกทีว่าเขียนแบบนี้ไหม
                string sql = "SELECT * FROM promotions WHERE status = 'Active'";
                return Ok(ExecuteQueryFromDb(sql));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // --- ฟังก์ชันตัวช่วยสำหรับรัน SQL แล้วแปลงผลลัพธ์เป็น JSON (ต้องมีไว้ในคลาสนี้) ---
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
}
