using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MvcProduct.UserInterface.Models;

namespace MvcProduct.Infrastructure.Data
{
    public class MvcFishContext : DbContext
    {
        public MvcFishContext (DbContextOptions<MvcFishContext> options)
            : base(options)
        {
        }

        public DbSet<Fishs> Fishs { get; set; } = default!;
    }
}
