using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;
using NeueVox.Model.NeuevoxModel.MongoDb;

namespace NeueVox.Model.NeuevoxModel.Context;

public class MongoDbContext : DbContext
{
  public DbSet<Quote> Quotes { get; set; }
  public DbSet<Announcement> Announcements { get; set; }

  public MongoDbContext(DbContextOptions<MongoDbContext> options)
    : base(options)
  {
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.Entity<Quote>().ToCollection("Quotes");
    base.OnModelCreating(modelBuilder);
    modelBuilder.Entity<Announcement>().ToCollection("Announcements");
  }
}
