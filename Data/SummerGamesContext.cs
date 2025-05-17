using DDivyansh_Project1.Models;
using Microsoft.EntityFrameworkCore;

public class SummerGamesContext : DbContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    //Property to hold the UserName value
    public string UserName
    {
        get; private set;
    }
    public SummerGamesContext(DbContextOptions<SummerGamesContext> options, IHttpContextAccessor httpContextAccessor)
            : base(options)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        if (_httpContextAccessor.HttpContext != null)
        {
            //We have a HttpContext, but there might not be anyone Authenticated
            UserName = _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "Unknown";
        }
        else
        {
            //No HttpContext so seeding data
            UserName = "Seed Data";
        }
    }
    public SummerGamesContext(DbContextOptions<SummerGamesContext> options)
     : base(options)
    {
        _httpContextAccessor = null!;
        UserName = "Seed Data";
    }
    public DbSet<Athlete> Athletes { get; set; }
    public DbSet<Contingent> Contingents { get; set; }
    public DbSet<Sport> Sports { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Unique Constraints
        modelBuilder.Entity<Athlete>()
            .HasIndex(a => a.AthleteCode)
            .IsUnique();

        modelBuilder.Entity<Contingent>()
            .HasIndex(c => c.Code)
            .IsUnique();

        modelBuilder.Entity<Sport>()
            .HasIndex(s => s.Code)
            .IsUnique();

        // Relationships
        modelBuilder.Entity<Athlete>()
            .HasOne(a => a.Contingent)
            .WithMany(c => c.Athletes)
            .HasForeignKey(a => a.ContingentID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Athlete>()
            .HasOne(a => a.Sport)
            .WithMany(s => s.Athletes)
            .HasForeignKey(a => a.SportID)
            .OnDelete(DeleteBehavior.Restrict);


    }
}
