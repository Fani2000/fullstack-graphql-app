
using Entities;
using Microsoft.EntityFrameworkCore;

namespace ApplicationContext;
public class CibContext : DbContext
{
    public CibContext(DbContextOptions<CibContext> options) : base(options) { }
    
    public DbSet<Company> Companies { get; set; }
    public DbSet<CompanyMember> CompanyMembers { get; set; }
    public DbSet<CompanyMemberPermission> CompanyMemberPermissions { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<SubProduct> SubProducts { get; set; }
}