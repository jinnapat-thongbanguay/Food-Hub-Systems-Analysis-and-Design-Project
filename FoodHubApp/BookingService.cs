using System;
using System.Data;
using Npgsql;

namespace FoodHubApp
{
    public class BookingService
    {
        // สร้าง Object เพื่อเชื่อมต่อไปยัง Data Tier (DatabaseManager)
        DatabaseManager db = new DatabaseManager();

        // ฟังก์ชันสำหรับหน้าแรก (Form1) เพื่อใช้ค้นหาร้านอาหาร
        public DataTable SearchRestaurants(string name)
        {
            string sql = "SELECT restaurantid, name, address, phone FROM restaurants WHERE name ILIKE @name";
            var parameters = new[] { new NpgsqlParameter("name", "%" + name + "%") };
            return db.ExecuteQuery(sql, parameters);
        }

        // ฟังก์ชันสำหรับหน้าจอง (BookingForm) เพื่อดึงโปรโมชันตาม ID ร้าน
        public DataTable GetPromotionsByRestaurant(int resId)
        {
            // ดึง startdate และ enddate แยกออกมาด้วย (สำคัญมาก!)
            string sql = @"SELECT promotionid, startdate, enddate,
                 name || ' [' || to_char(startdate, 'DD/MM/YY') || ' - ' || to_char(enddate, 'DD/MM/YY') || ']' as full_display 
                 FROM promotions 
                 WHERE restaurantid = @rid AND status = 'Active'";

            var param = new[] { new Npgsql.NpgsqlParameter("rid", resId) };
            return db.ExecuteQuery(sql, param);
        }

        // ฟังก์ชันยืนยันการจอง บันทึกลง pgAdmin พร้อม Fix User ID = 1
        public bool ConfirmBooking(int resId, int promoId, DateTime date, int people)
        {
            // ตามเกณฑ์ที่อาจารย์กำหนดใน PDF: ให้ Fix User ได้เลย
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
    } // ปิด Class
} // ปิด Namespace