using RoomBookingApp.Core.Models;
using RoomBookingApp.Core.Processors;
using Shouldly;

namespace RoomBookingApp.Core.Tests
{
    public class RoomBookingRequestProcessorTest
    {
        private readonly RoomBookingRequestProcessor _processor;

        public RoomBookingRequestProcessorTest()
        {
            // Se os testes estão toda hora utilizando o mesmo objeto instanciar uma vez
            // só no construtor para não ficar repetindo código toda hora, como abaixo:
            // Usando o Arrange uma vez só e reaproveitando
            _processor = new RoomBookingRequestProcessor();
        }

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

            // Act
            RoomBookingResult result = _processor.BookRoom(bookingRequest);

            // Assert
            Assert.NotNull(result); // garantir que o resultado não é nulo
            Assert.Equal(bookingRequest.FullName, result.FullName);
            Assert.Equal(bookingRequest.Email, result.Email);
            Assert.Equal(bookingRequest.Date, result.Date);


            result.ShouldNotBeNull(); // mesma coisa com o pacote shouldly, onde podemos adicionar mensagens customizadas
            result.FullName.ShouldBe(bookingRequest.FullName);
            result.Email.ShouldBe(bookingRequest.Email);
            result.Date.ShouldBe(bookingRequest.Date);
        }

        [Fact]
        public void Should_Throw_Exception_For_Null_Request()
        {
            var exception = Should.Throw<ArgumentNullException>(() => _processor.BookRoom(null));  // Utilizando o Shoudly
            // Assert.Throws<ArgumentNullException>(() => _processor.BookRoom(null)); // Utilizando Assert

            exception.ParamName.ShouldBe("bookingRequest");
            // Assert.Equal(exception.ParamName, "bookingRequest");
        }
    }
}
