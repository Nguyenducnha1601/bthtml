using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MvcMovie.Models.Entities;

namespace MvcMovie.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }
        public DbSet<Person> Person { get; set; }
        public DbSet<HeThongPhanPhoi> HeThongPhanPhoiHeThongPhanPhoi { get; set; }
        public DbSet<DaiLy> DaiLy { get; set; } = default!;
        public DbSet<MvcMovie.Models.Entities.Employee> Employee { get; set; } = default!;
        public DbSet<MvcMovie.Models.Entities.MemberUnit> MemberUnit { get; set; } = default!;
  
    }
}