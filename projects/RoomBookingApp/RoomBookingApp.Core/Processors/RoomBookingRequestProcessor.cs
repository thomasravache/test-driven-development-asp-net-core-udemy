using RoomBookingApp.Core.DataServices;
using RoomBookingApp.Core.Models;

namespace RoomBookingApp.Core.Processors
{
    public class RoomBookingRequestProcessor
    {
        public RoomBookingRequestProcessor(IRoomBookingService roomBookingService)
        {
        }

        public RoomBookingResult BookRoom(RoomBookingRequest bookingRequest)
        {
            if (bookingRequest is null)
                throw new ArgumentNullException(nameof(bookingRequest));

            return new RoomBookingResult()
            {
                FullName = bookingRequest.FullName,
                Date = bookingRequest.Date,
                Email = bookingRequest.Email
            };
        }
    }
}