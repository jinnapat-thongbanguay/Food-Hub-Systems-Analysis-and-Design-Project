using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System;
using System.Collections.Generic;

namespace FoodHubBackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManageBookingsController : ControllerBase
    {
        private readonly string _connString;

        public ManageBookingsController(IConfiguration configuration)
        {
            _connString = configuration.GetConnectionString("DefaultConnection");
        }

        public class MyBookingModel
        {
            public int bookingid { get; set; }
            public string restaurant_name { get; set; }
            public string bookingdate { get; set; }
            public int numberofpeople { get; set; }
            public string status { get; set; }
        }

        [HttpGet("{phone}")]
        public IActionResult GetBookingsByPhone(string phone)
        {
            try
            {
                var list = new List<MyBookingModel>();
                using (var conn = new NpgsqlConnection(_connString))
                {
                    conn.Open();
                    // JOIN 3 ตาราง: bookings (หลัก) + customers (หาเบอร์) + restaurants (หาชื่อร้าน)
                    string query = @"
                        SELECT b.bookingid, r.name AS restaurant_name, b.bookingdate, b.numberofpeople, b.status 
                        FROM public.bookings b
                        JOIN public.customers c ON b.customerid = c.customerid
                        JOIN public.restaurants r ON b.restaurantid = r.restaurantid
                        WHERE c.phone = @phone
                        ORDER BY b.bookingdate DESC";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("phone", phone);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DateTime bDate = Convert.ToDateTime(reader["bookingdate"]);
                                list.Add(new MyBookingModel
                                {
                                    bookingid = Convert.ToInt32(reader["bookingid"]),
                                    restaurant_name = reader["restaurant_name"]?.ToString() ?? "",
                                    bookingdate = bDate.ToString("dd/MM/yyyy HH:mm"), // จัดฟอร์แมตให้ฝั่งวินฟอร์มเลย
                                    numberofpeople = reader["numberofpeople"] != DBNull.Value ? Convert.ToInt32(reader["numberofpeople"]) : 0,
                                    status = reader["status"]?.ToString() ?? ""
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

        [HttpPut("cancel/{bookingId}")]
        public IActionResult CancelBooking(int bookingId)
        {
            try
            {
                using (var conn = new NpgsqlConnection(_connString))
                {
                    conn.Open();
                    string query = "UPDATE public.bookings SET status = 'Cancelled' WHERE bookingid = @bookingId";
                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("bookingId", bookingId);
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                            return Ok(new { message = "ยกเลิกการจองสำเร็จ" });
                        else
                            return NotFound(new { message = "ไม่พบข้อมูลการจองนี้" });
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Database Error: {ex.Message}");
            }
        }
    }
}