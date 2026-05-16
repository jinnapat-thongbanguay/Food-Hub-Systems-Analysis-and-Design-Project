using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System;
using System.Collections.Generic;

namespace FoodHubBackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionsController : ControllerBase
    {
        private readonly string _connString;

        public PromotionsController(IConfiguration configuration)
        {
            _connString = configuration.GetConnectionString("DefaultConnection");
        }

        public class PromotionModel
        {
            public int promotionid { get; set; }
            public string name { get; set; }
            public string description { get; set; }
            public DateTime startdate { get; set; }
            public DateTime enddate { get; set; }
        }

        [HttpGet("restaurant/{resId}")]
        public IActionResult GetPromotions(int resId)
        {
            try
            {
                var list = new List<PromotionModel>();
                using (var conn = new NpgsqlConnection(_connString))
                {
                    conn.Open();
                    string query = @"
                        SELECT promotionid, name, description, startdate, enddate 
                        FROM public.promotions 
                        WHERE restaurantid = @resId AND enddate >= CURRENT_DATE";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("resId", resId);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(new PromotionModel
                                {
                                    promotionid = Convert.ToInt32(reader["promotionid"]),
                                    name = reader["name"]?.ToString() ?? "",
                                    description = reader["description"]?.ToString() ?? "",
                                    startdate = Convert.ToDateTime(reader["startdate"]),
                                    enddate = Convert.ToDateTime(reader["enddate"])
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