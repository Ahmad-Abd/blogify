using Blogify.Common;
using Blogify.Common.DbContexts;
using Blogify.Test.Common;
using Microsoft.EntityFrameworkCore;

namespace Blogify.EntityFrameworkCore.Test.Common.DbContexts;

public class TestDbContextBase : DbContextBase
{
    #region Test Db Sets

    public DbSet<TestEntity> TestEntities { get; set; } = null!;

    #endregion
    
    protected TestDbContextBase()
    {
    }

    public TestDbContextBase(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TestEntity>(b=>
        {
            b.HasKey(e => e.Id);
            b.Property(e => e.Description).HasMaxLength(256);
        });
        base.OnModelCreating(modelBuilder);
    }
}