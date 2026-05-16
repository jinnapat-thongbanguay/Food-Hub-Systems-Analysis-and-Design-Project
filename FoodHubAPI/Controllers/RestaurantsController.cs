using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System;
using System.Collections.Generic;

namespace FoodHubAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        private readonly string _connString;

        // เชื่อมสายดึง Connection String มาจากไฟล์ appsettings.json
        public RestaurantsController(IConfiguration configuration)
        {
            _connString = configuration.GetConnectionString("DefaultConnection");
        }

        // โมเดลล็อกหัวคอลัมน์ภาษาอังกฤษตาม pgAdmin ของหนู
        public class RestaurantModel
        {
            public int restaurantid { get; set; }
            public string name { get; set; }
            public string address { get; set; }
            public string phone { get; set; }
        }

        // 🟢 เปิดท่อ HTTP GET: api/Restaurants
        [HttpGet]
        public IActionResult GetRestaurants([FromQuery] string keyword = "")
        {
            try
            {
                var list = new List<RestaurantModel>();

                using (var conn = new NpgsqlConnection(_connString))
                {
                    conn.Open();

                    // สั่งคิวรีดึงข้อมูลจากตาราง public.restaurants ของแท้
                    string query = @"
                        SELECT restaurantid, name, address, phone 
                        FROM public.restaurants 
                        WHERE name ILIKE @search 
                        ORDER BY name";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("search", $"%{keyword}%");
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(new RestaurantModel
                                {
                                    restaurantid = Convert.ToInt32(reader["restaurantid"]),
                                    name = reader["name"]?.ToString() ?? "",
                                    address = reader["address"]?.ToString() ?? "",
                                    phone = reader["phone"]?.ToString() ?? ""
                                });
                            }
                        }
                    }
                }
                return Ok(list); // ส่งผลลัพธ์เป็นก้อน JSON สะอาดๆ ออกไป
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Database Error: {ex.Message}");
            }
        }
    }
}