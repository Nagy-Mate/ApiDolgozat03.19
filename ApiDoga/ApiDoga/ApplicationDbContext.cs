using ApiDoga.Core;
using Microsoft.EntityFrameworkCore;

namespace ApiDoga;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Product> Orders { get; set; }
}
