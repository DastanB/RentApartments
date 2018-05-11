using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;


namespace Final.Models 
{
    public class FinalDbContext: DbContext 
    {
        public virtual DbSet<Apartment> Apartments { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public FinalDbContext(DbContextOptions<FinalDbContext> options) : base(options)
        {
        }

    }
}