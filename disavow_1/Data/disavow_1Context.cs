using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace disavow_1.Models
{
    public class disavow_1Context : DbContext
    {
        public disavow_1Context (DbContextOptions<disavow_1Context> options)
            : base(options)
        {
        }

        public DbSet<disavow_1.Models.Disavow> Disavow { get; set; }
    }
}
