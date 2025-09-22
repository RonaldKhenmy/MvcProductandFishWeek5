using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MvcProduct.UserInterface.Models;

namespace MvcProduct.Infrastructure.Data
{
    public class MvcProductContext : DbContext
    {
        public MvcProductContext (DbContextOptions<MvcProductContext> options)
            : base(options)
        {
        }

        public DbSet<Products> Products { get; set; } = default!;
    }
}
