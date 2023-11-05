using Microsoft.EntityFrameworkCore;
using StudentApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp.Infrastructure
{
    public class DbContextCore : DbContext
    {
        public DbContextCore(DbContextOptions<DbContextCore> options):base (options) { }

        public DbSet<Student> tbl_Students { get; set; }
    }
}
