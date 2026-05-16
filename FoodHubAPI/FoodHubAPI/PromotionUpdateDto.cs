namespace FoodHubAPI.Models // เปลี่ยนชื่อ namespace ให้ตรงกับโปรเจกต์ของคุณ
{
    public class PromotionUpdateDto
    {
        public string Name { get; set; }
        public decimal DiscountAmount { get; set; }
        public string Status { get; set; }
    }
}