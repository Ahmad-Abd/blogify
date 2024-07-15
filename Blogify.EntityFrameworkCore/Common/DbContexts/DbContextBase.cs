using Microsoft.EntityFrameworkCore;

namespace Blogify.Common.DbContexts;

public class DbContextBase:DbContext
{
    protected DbContextBase()
    {
    }

    public DbContextBase(DbContextOptions options) : base(options)
    {
    }
}