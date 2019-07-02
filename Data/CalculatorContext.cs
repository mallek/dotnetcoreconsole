using Microsoft.EntityFrameworkCore;

namespace SkunkCalc.Data
{
    public class CalculatorContext : DbContext
    {
        public CalculatorContext(DbContextOptions<CalculatorContext> options)
                    : base(options)
        { }

        public DbSet<Sale> Blogs { get; set; }
    }
}