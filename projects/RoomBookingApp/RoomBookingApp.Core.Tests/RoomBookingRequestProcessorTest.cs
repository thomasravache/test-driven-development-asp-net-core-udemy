using Moq;
using RoomBookingApp.Core.DataServices;
using RoomBookingApp.Core.Domain;
using RoomBookingApp.Core.Models;
using RoomBookingApp.Core.Processors;
using Shouldly;

namespace RoomBookingApp.Core.Tests
{
    public class RoomBookingRequestProcessorTest
    {
        private readonly RoomBookingRequestProcessor _processor;
        private readonly RoomBookingRequest _request;
        private Mock<IRoomBookingService> _roomBookingServiceMock;

        public RoomBookingRequestProcessorTest()
        {
            // Se os testes estão toda hora utilizando o mesmo objeto instanciar uma vez
            // só no construtor para não ficar repetindo código toda hora, como abaixo:
            // Usando o Arrange uma vez só e reaproveitando

            // Arrange
            _request = new RoomBookingRequest
            {
                FullName = "Test Name",
                Email = "test@request.com",
                Date = new DateTime(2021, 10, 20),
            };

            _roomBookingServiceMock = new Mock<IRoomBookingService>();
            _processor = new RoomBookingRequestProcessor(_roomBookingServiceMock.Object);
        }

        [Fact]
        public void Should_Return_Room_Booking_Response_With_Request_Values() // pode ser em PascalCase também, tudo depende
        {
            // Act
            RoomBookingResult result = _processor.BookRoom(_request);

            // Assert
            Assert.NotNull(result); // garantir que o resultado não é nulo
            Assert.Equal(_request.FullName, result.FullName);
            Assert.Equal(_request.Email, result.Email);
            Assert.Equal(_request.Date, result.Date);


            result.ShouldNotBeNull(); // mesma coisa com o pacote shouldly, onde podemos adicionar mensagens customizadas
            result.FullName.ShouldBe(_request.FullName);
            result.Email.ShouldBe(_request.Email);
            result.Date.ShouldBe(_request.Date);
        }

        [Fact]
        public void Should_Throw_Exception_For_Null_Request()
        {
            var exception = Should.Throw<ArgumentNullException>(() => _processor.BookRoom(null));  // Utilizando o Shoudly
            // Assert.Throws<ArgumentNullException>(() => _processor.BookRoom(null)); // Utilizando Assert

            exception.ParamName.ShouldBe("bookingRequest");
            // Assert.Equal(exception.ParamName, "bookingRequest");
        }

        [Fact]
        public void Should_Save_Room_Booking_Request()
        {
            RoomBooking savedBooking = null;

            // Aqui estamos dizendo que quando o serviço for chamado,
            // estamos dizendo que queremos que ele se comporte dessa forma ao chamar
            // o método Save, estamos configurando o coportamento do método Save
            // simulando que um objeto do tipo RoomBooking seja passado corretamente
            _roomBookingServiceMock.Setup(q => q.Save(It.IsAny<RoomBooking>()))
                .Callback<RoomBooking>(booking =>
                {
                    savedBooking = booking;
                });

            _processor.BookRoom(_request);

            // Aqui está sendo verificado que após o processor chamar o método BookRoom que 
            // lá dentro o método de Save do serviço seja chamado apenas uma vez
            _roomBookingServiceMock.Verify(q => q.Save(It.IsAny<RoomBooking>()), Times.Once);

            savedBooking.ShouldNotBeNull();
            savedBooking.FullName.ShouldBe(_request.FullName);
            savedBooking.Email.ShouldBe(_request.Email);
            savedBooking.Date.ShouldBe(_request.Date);
        }
    }
}
