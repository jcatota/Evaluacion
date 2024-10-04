using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Context
{
    public class TestDBCntext : DbContext
    {
        public TestDBCntext(DbContextOptions<TestDBCntext> options)
        : base(options)
        {
        }

        public DbSet<Persona> Persona { get; set; }
        public DbSet<Pet> Pet{ get; set; }

    }
}
