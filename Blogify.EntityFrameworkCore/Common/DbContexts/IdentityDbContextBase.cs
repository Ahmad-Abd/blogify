using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blogify.Common.DbContexts;

public class IdentityDbContextBase:IdentityDbContext
{
    public IdentityDbContextBase(DbContextOptions options) : base(options)
    {
    }

    protected IdentityDbContextBase()
    {
    }
}