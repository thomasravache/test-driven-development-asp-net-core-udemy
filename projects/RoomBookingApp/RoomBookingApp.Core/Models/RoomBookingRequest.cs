namespace RoomBookingApp.Core.Models
{
    public class RoomBookingRequest
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime Date { get; set; }
    }
}