using System;
using System.Data;
using Npgsql;

namespace FoodHubApp
{
    public class BookingService
    {
        // เชื่อมต่อกับ Data Tier
        DatabaseManager db = new DatabaseManager();

        // 1. ค้นหาร้านอาหาร (สำหรับหน้าแรก Form1)
        public DataTable SearchRestaurants(string name)
        {
            string sql = "SELECT restaurantid, name, address, phone FROM restaurants WHERE name ILIKE @name";
            var parameters = new[] { new NpgsqlParameter("name", "%" + name + "%") };
            return db.ExecuteQuery(sql, parameters);
        }

        // 2. ดึงโปรโมชันตามร้าน (สำหรับหน้า BookingForm)
        public DataTable GetPromotionsByRestaurant(int resId)
        {
            string sql = @"SELECT promotionid, startdate, enddate,
                 name || ' [' || to_char(startdate, 'DD/MM/YY') || ' - ' || to_char(enddate, 'DD/MM/YY') || ']' as full_display 
                 FROM promotions 
                 WHERE restaurantid = @rid AND status = 'Active'";

            var param = new[] { new NpgsqlParameter("rid", resId) };
            return db.ExecuteQuery(sql, param);
        }

        // 3. บันทึกข้อมูลลูกค้าพร้อมการจอง (สำหรับหน้า CustomerForm)
        // ใช้หลักการดึง ID จากตาราง customers ไปใส่ในตาราง bookings
        public bool CreateCustomerAndBooking(string name, string email, string phone, int rid, int pid, DateTime date, int people)
        {
            try
            {
                // บันทึกข้อมูลลูกค้าและดึง customerid กลับมาทันที
                string sqlCust = "INSERT INTO customers (fullname, email, phone) VALUES (@name, @email, @phone) RETURNING customerid";
                var paramCust = new[] {
                    new NpgsqlParameter("name", name),
                    new NpgsqlParameter("email", email),
                    new NpgsqlParameter("phone", phone)
                };

                DataTable dt = db.ExecuteQuery(sqlCust, paramCust);
                if (dt.Rows.Count == 0) return false;

                int newCustId = Convert.ToInt32(dt.Rows[0]["customerid"]);

                // บันทึกการจองโดยเชื่อมโยงกับ ID ลูกค้าที่เพิ่งสร้าง
                string sqlBook = @"INSERT INTO bookings (customerid, restaurantid, promotionid, bookingdate, numberofpeople, status) 
                                  VALUES (@cid, @rid, @pid, @date, @people, 'Confirmed')";
                var paramBook = new[] {
                    new NpgsqlParameter("cid", newCustId),
                    new NpgsqlParameter("rid", rid),
                    new NpgsqlParameter("pid", pid),
                    new NpgsqlParameter("date", date),
                    new NpgsqlParameter("people", people)
                };

                return db.ExecuteNonQuery(sqlBook, paramBook);
            }
            catch { return false; }
        }

        // 4. ดึงรีวิวของร้านอาหาร (สำหรับหน้า ReviewForm)
        // แก้ไข Error CS0111 โดยรวมให้เหลือฟังก์ชันเดียว และเรียงลำดับวันที่ล่าสุดขึ้นก่อน
        public DataTable GetReviewsByRestaurant(int resId)
        {
            string sql = "SELECT rating, comment, reviewdate FROM reviews WHERE restaurantid = @rid ORDER BY reviewdate DESC";
            var param = new[] { new NpgsqlParameter("rid", resId) };
            return db.ExecuteQuery(sql, param);
        }

        // เพิ่มกลับเข้าไปเพื่อให้ BookingForm เรียกใช้งานได้
        public bool ConfirmBooking(int resId, int promoId, DateTime date, int people)
        {
            try
            {
                // Fix customerid = 1 สำหรับเคสที่ไม่ต้องการกรอกชื่อใหม่
                string sql = @"INSERT INTO bookings (customerid, restaurantid, promotionid, bookingdate, numberofpeople, status) 
                     VALUES (1, @rid, @pid, @date, @people, 'Confirmed')";

                var params_list = new[] {
            new NpgsqlParameter("rid", resId),
            new NpgsqlParameter("pid", promoId),
            new NpgsqlParameter("date", date),
            new NpgsqlParameter("people", people)
        };
                return db.ExecuteNonQuery(sql, params_list);
            }
            catch { return false; }
        }

    } // ปิด Class
} // ปิด Namespace