using Microsoft.EntityFrameworkCore;
using LabEquipmentApi.Domain;

namespace LabEquipmentApi.Data;

public class LabDbContext : DbContext
{
    public DbSet<Equipment> EquipmentItems => Set<Equipment>();

    public LabDbContext(DbContextOptions<LabDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Equipment>().HasData(
            new Equipment { Id = 1, Name = "Projektor", Room = "Lab 10", IsAvailable = true },
            new Equipment { Id = 2, Name = "Laptop", Room = "Lab 10", IsAvailable = false },
            new Equipment { Id = 3, Name = "Monitor", Room = "Lab 11", IsAvailable = true }
        );
    }
}
}