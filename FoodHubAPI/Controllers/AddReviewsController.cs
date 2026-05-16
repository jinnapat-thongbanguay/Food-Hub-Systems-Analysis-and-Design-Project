using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System;
using System.Collections.Generic;

namespace FoodHubBackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddReviewsController : ControllerBase
    {
        private readonly string _connString;

        public AddReviewsController(IConfiguration configuration)
        {
            _connString = configuration.GetConnectionString("DefaultConnection");
        }

        // โมเดลสำหรับดึงข้อมูลการจองที่ทานเสร็จแล้วมาโชว์
        public class CompletedBookingModel
        {
            public int bookingid { get; set; }
            public int customerid { get; set; }
            public string restaurantname { get; set; }
            public int restaurantid { get; set; }
            public string bookingdate { get; set; }
            public string comment { get; set; }
            public string rating { get; set; }
        }

        // GET: ดึงประวัติการเข้าทานอาหารด้วยเบอร์โทร
        [HttpGet("{phone}")]
        public IActionResult GetCompletedBookings(string phone)
        {
            try
            {
                var list = new List<CompletedBookingModel>();
                using (var conn = new NpgsqlConnection(_connString))
                {
                    conn.Open();
                    string query = @"
                        SELECT b.bookingid, c.customerid, r.name AS restaurantname, b.restaurantid, b.bookingdate, rev.comment, rev.rating
                        FROM public.bookings b
                        JOIN public.customers c ON b.customerid = c.customerid
                        JOIN public.restaurants r ON b.restaurantid = r.restaurantid
                        LEFT JOIN public.reviews rev ON b.customerid = rev.customerid AND b.restaurantid = rev.restaurantid
                        WHERE c.phone = @phone AND b.status = 'Completed'
                        ORDER BY b.bookingdate DESC";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("phone", phone);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(new CompletedBookingModel
                                {
                                    bookingid = Convert.ToInt32(reader["bookingid"]),
                                    customerid = Convert.ToInt32(reader["customerid"]),
                                    restaurantname = reader["restaurantname"]?.ToString() ?? "",
                                    restaurantid = Convert.ToInt32(reader["restaurantid"]),
                                    bookingdate = Convert.ToDateTime(reader["bookingdate"]).ToString("dd/MM/yyyy HH:mm"),
                                    comment = reader["comment"]?.ToString() ?? "",
                                    rating = reader["rating"] != DBNull.Value ? reader["rating"].ToString() : ""
                                });
                            }
                        }
                    }
                }

                if (list.Count == 0) return NotFound("ไม่พบประวัติการจองที่ทานเสร็จเรียบร้อยแล้ว");
                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Database Error: {ex.Message}");
            }
        }

        // โมเดลสำหรับรับข้อมูลรีวิวใหม่จากวินฟอร์ม
        public class NewReviewRequest
        {
            public int customerId { get; set; }
            public int restaurantId { get; set; }
            public int bookingId { get; set; }
            public int rating { get; set; }
            public string comment { get; set; }
        }

        // POST: บันทึกคอมเมนต์และคะแนนลงตาราง reviews
        [HttpPost]
        public IActionResult AddReview([FromBody] NewReviewRequest req)
        {
            try
            {
                using (var conn = new NpgsqlConnection(_connString))
                {
                    conn.Open();
                    string insertQuery = @"
                        INSERT INTO public.reviews (customerid, restaurantid, bookingid, rating, comment, reviewdate) 
                        VALUES (@cid, @rid, @bid, @rate, @comment, CURRENT_TIMESTAMP)";

                    using (var cmd = new NpgsqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("cid", req.customerId);
                        cmd.Parameters.AddWithValue("rid", req.restaurantId);
                        cmd.Parameters.AddWithValue("bid", req.bookingId);
                        cmd.Parameters.AddWithValue("rate", req.rating);
                        cmd.Parameters.AddWithValue("comment", req.comment);

                        cmd.ExecuteNonQuery();
                    }
                }
                return Ok(new { message = "บันทึกรีวิวสำเร็จ" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Database Error: {ex.Message}");
            }
        }
    }
}