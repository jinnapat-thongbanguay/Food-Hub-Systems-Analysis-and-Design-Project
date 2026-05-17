using System;

namespace FoodHubAPI.Models
{
    public class BookingUpdateDto
    {
        public int BookingId { get; set; }
        public int CustomerId { get; set; }
        public int RestaurantId { get; set; }
        public DateTime BookingDate { get; set; }
        public string Status { get; set; } = string.Empty;
}
}