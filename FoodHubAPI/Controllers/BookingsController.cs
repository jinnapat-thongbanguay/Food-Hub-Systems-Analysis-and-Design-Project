using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System;

namespace FoodHubBackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly string _connString;

        public BookingsController(IConfiguration configuration)
        {
            _connString = configuration.GetConnectionString("DefaultConnection");
        }

        public class BookingRequest
        {
            public string fullName { get; set; }
            public string email { get; set; }
            public string phone { get; set; }
            public int restaurantId { get; set; }
            public string bookingDateString { get; set; }
            public int numberOfPeople { get; set; }
            public int? promotionId { get; set; }
        }

        [HttpPost]
        public IActionResult CreateBooking([FromBody] BookingRequest req)
        {
            try
            {
                if (!DateTime.TryParseExact(req.bookingDateString, "dd/MM/yyyy HH:mm",
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None, out DateTime finalBookingDate))
                {
                    return BadRequest("รูปแบบวันที่และเวลาไม่ถูกต้อง");
                }

                using (var conn = new NpgsqlConnection(_connString))
                {
                    conn.Open();

                    // PostgreSQL (CTE): บันทึกลูกค้าใหม่ลง customers ก่อน
                    // แล้วดึง customerid ที่เพิ่งได้ มาบันทึกการจองลงตาราง bookings ทันทีแบบไร้รอยต่อ!
                    string query = @"
                        WITH new_customer AS (
                            INSERT INTO public.customers (fullname, email, phone) 
                            VALUES (@fullName, @email, @phone) 
                            RETURNING customerid
                        )
                        INSERT INTO public.bookings (customerid, restaurantid, bookingdate, status, createat, numberofpeople, promotionid) 
                        SELECT customerid, @restaurantId, @bookingDate, @status, @createAt, @numberOfPeople, @promotionId 
                        FROM new_customer;";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        // ข้อมูลฝั่งลูกค้า
                        cmd.Parameters.AddWithValue("fullName", req.fullName);
                        cmd.Parameters.AddWithValue("email", req.email);
                        cmd.Parameters.AddWithValue("phone", req.phone);

                        // ข้อมูลฝั่งการจอง
                        cmd.Parameters.AddWithValue("restaurantId", req.restaurantId);
                        cmd.Parameters.AddWithValue("bookingDate", finalBookingDate);
                        cmd.Parameters.AddWithValue("status", "NewRequest");
                        cmd.Parameters.AddWithValue("createAt", DateTime.Now);
                        cmd.Parameters.AddWithValue("numberOfPeople", req.numberOfPeople);
                        cmd.Parameters.AddWithValue("promotionId", (object)req.promotionId ?? DBNull.Value);

                        cmd.ExecuteNonQuery();
                    }
                }
                return Ok(new { message = "บันทึกการจองสำเร็จ" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Database Error: {ex.Message}");
            }
        }
    }
}