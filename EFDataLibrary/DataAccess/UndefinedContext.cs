using EFDataLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFDataLibrary.DataAccess
{
    public class UndefinedContext:DbContext
    {
        public UndefinedContext(DbContextOptions options) : base(options) { }
        public DbSet<Product> Products { get; set; }
    }
}
