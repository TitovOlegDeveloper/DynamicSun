using DynamicSun.Dal.MsSql.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicSun.Dal.MsSql.Context
{
    public class MSContext : DbContext
    {
        public DbSet<Weather> Weathers { get; set; }

     
        public MSContext(DbContextOptions options) : base(options)
        {

        }

        public MSContext()
        {

        }

        
    }
}
