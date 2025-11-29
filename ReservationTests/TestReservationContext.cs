// TestReservationContext.cs
using Microsoft.EntityFrameworkCore;
using ReservationService.Models;

namespace ReservationTests
{
    public class TestReservationContext : DbContext
    {
        public TestReservationContext(DbContextOptions<TestReservationContext> options) : base(options) { }

        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Базовая конфигурация если нужна
            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.HasKey(e => e.Id);
            });
        }
    }
}