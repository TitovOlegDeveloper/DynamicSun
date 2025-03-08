using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicSun.Domain.Entities
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Weather> Weathers { get; set; }    

        public ApplicationContext(DbContextOptions options) : base(options) 
        {

        }
    }
}
