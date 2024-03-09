namespace RoomBookingApp.Core.Models;

public abstract class RoomBookingBase
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime Date { get; set; }
}