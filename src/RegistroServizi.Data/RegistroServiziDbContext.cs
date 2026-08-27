using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RegistroServizi.Data.Identity;

namespace RegistroServizi.Data;

public class RegistroServiziDbContext(DbContextOptions<RegistroServiziDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RegistroServiziDbContext).Assembly);
    }
}
