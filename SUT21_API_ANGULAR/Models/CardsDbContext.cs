using Microsoft.EntityFrameworkCore;

namespace SUT21_API_ANGULAR.Models
{
    public class CardsDbContext : DbContext
    {

        public CardsDbContext(DbContextOptions options) :base(options)  
        {

        }


        // Tabel

        public DbSet<Card> Cards { get; set; }
    }
}
