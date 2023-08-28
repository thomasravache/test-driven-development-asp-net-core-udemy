using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomBookingApp.Core.Tests
{
    public class RoomBookingRequestProcessorTest
    {
        [Fact]
        public void Should_Return_Room_Booking_Response_With_Request_Values() // pode ser em PascalCase também, tudo depende
        {
            // Arrange
            var bookingRequest = new RoomBookingRequest
            {
                FullName = "Test Name",
                Email = "test@request.com",
                Date = new DateTime(2021, 10, 20),
            };

            var processor = new RoomBookingRequestProcessor();

            // Act
            RoomBookingResult result = processor.BookRoom(bookingRequest);

            // Assert
            Assert.NotNull(result); // garantir que o resultado não é nulo
            Assert.Equal(bookingRequest.FullName, result.FullName);
            Assert.Equal(bookingRequest.Email, result.Email);
            Assert.Equal(bookingRequest.Date, result.Date);


            result.ShouldNotBeNull(); // mesma coisa com o pacote shouldly, onde podemos adicionar mensagens customizadas
            result.FullName.ShouldBe(result.FullName);
            result.Email.ShouldBe(result.Email);
            result.Date.ShouldBe(result.Date);
        }
    }
}
