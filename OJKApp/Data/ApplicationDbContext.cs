using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OJKApp.Models;

namespace OJKApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<OJKApp.Models.MataKuliahViewModel> MataKuliahViewModel { get; set; } = default!;
        public DbSet<OJKApp.Models.DosenViewModel> DosenViewModel { get; set; } = default!;
        public DbSet<OJKApp.Models.MahasiswaViewModel> MahasiswaViewModel { get; set; } = default!;
    }
}
