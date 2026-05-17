using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System;
using System.Collections.Generic;

namespace FoodHubBackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly string _connString;

        public ReviewsController(IConfiguration configuration)
        {
            _connString = configuration.GetConnectionString("DefaultConnection");
        }

        public class ReviewModel
        {
            public int reviewid { get; set; }
            public int restaurantid { get; set; }
            public string reviewername { get; set; } // ยังคงใช้ชื่อตัวแปรนี้เพื่อส่งให้วินฟอร์ม
            public string comment { get; set; }
            public int rating { get; set; }
        }

        [HttpGet("{restaurantId}")]
        public IActionResult GetReviews(int restaurantId)
        {
            try
            {
                var list = new List<ReviewModel>();

                using (var conn = new NpgsqlConnection(_connString))
                {
                    conn.Open();

                    // จุดเปลี่ยนสำคัญ: ใช้ LEFT JOIN เชื่อมตาราง reviews เข้ากับตาราง customers 
                    // เพื่อดึง fullname มาแปลงร่างเป็น reviewername ให้วินฟอร์มเอาไปโชว์
                    string query = @"
                        SELECT 
                            r.reviewid, 
                            r.restaurantid, 
                            COALESCE(c.fullname, 'ลูกค้าทั่วไป') AS reviewername, 
                            r.comment, 
                            r.rating 
                        FROM public.reviews r
                        LEFT JOIN public.customers c ON r.customerid = c.customerid
                        WHERE r.restaurantid = @resId 
                        ORDER BY r.reviewid DESC";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("resId", restaurantId);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(new ReviewModel
                                {
                                    reviewid = Convert.ToInt32(reader["reviewid"]),
                                    restaurantid = Convert.ToInt32(reader["restaurantid"]),
                                    reviewername = reader["reviewername"]?.ToString() ?? "ไม่ระบุชื่อ",
                                    comment = reader["comment"]?.ToString() ?? "ไม่มีข้อความ",
                                    rating = reader["rating"] != DBNull.Value ? Convert.ToInt32(reader["rating"]) : 0
                                });
                            }
                        }
                    }
                }

                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Database Error: {ex.Message}");
            }
        }
    }
}