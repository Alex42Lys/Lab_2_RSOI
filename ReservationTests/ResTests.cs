using Microsoft.EntityFrameworkCore;
using ReservationService;
using ReservationService.Models;
using Xunit;

namespace ReservationTests
{
    public class ResTests : IDisposable
    {
        private readonly TestReservationContext _context;
        private readonly ReservationRepository _repo;

        public ResTests()
        {
            var options = new DbContextOptionsBuilder<TestReservationContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new TestReservationContext(options);
            _repo = new ReservationRepository(_context); // ≈сли репозиторий принимает только PostgresContext, нужно адаптировать
        }

        [Fact]
        public async Task GetAllUserReservations_NoReservations_ReturnsEmptyList()
        {
            // Act
            var result = await _repo.GetAllUserReservations("a");

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task CreateNewReservation_ValidData_CreatesReservation()
        {
            // Arrange
            var userName = "testuser";
            var bookId = Guid.NewGuid();
            var libId = Guid.NewGuid();

            // Act
            var result = await _repo.CreateNewReservation(userName, bookId, libId, DateTime.Now, DateTime.Now.AddDays(7));

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userName, result.Username);
            Assert.Equal("RENTED", result.Status);

            // ѕровер€ем что запись действительно сохранилась
            var fromDb = await _context.Reservations.FirstAsync();
            Assert.Equal(userName, fromDb.Username);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}