using Microsoft.EntityFrameworkCore;
using System;
using Shared.Entities;

namespace WebApplication.Data
{
    public class AdventureworksContext: DbContext
    {
        public DbSet<BusinessEntityAddress> BusinessEntityAddresses { get; set; }
        public DbSet<PersonPhone> PersonPhones { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<EmailAddress> EmailAddresses { get; set; }
        public DbSet<Shared.Entities.Address> Addresses { get; set; }

        public AdventureworksContext(DbContextOptions<AdventureworksContext> options)
       : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity< BusinessEntityAddress>()
                .ToTable("BusinessEntityAddress", "Person");
            modelBuilder.Entity<PersonPhone>()
                .ToTable("PersonPhone", "Person");
            modelBuilder.Entity<Person>()
                .ToTable("Person", "Person", tb => tb.UseSqlOutputClause(false));                
            modelBuilder.Entity<EmailAddress>()
                .ToTable("EmailAddress", "Person");
            modelBuilder.Entity<Address>()
                .ToTable("Address", "Person");

            // Person
            modelBuilder.Entity<Person>()
                .HasKey(p => p.BusinessEntityID);
                
            // EmailAddress
            modelBuilder.Entity<EmailAddress>()
                .HasKey(e => e.EmailAddressID);

            // PersonPhone
            modelBuilder.Entity<PersonPhone>()
                .HasKey(pp => new { pp.BusinessEntityID, pp.PhoneNumber });

            // Address
            modelBuilder.Entity<Address>()
                .HasKey(a => a.AddressID);

            modelBuilder.Entity<EmailAddress>()
        .HasOne(e => e.Person)
        .WithMany(p => p.EmailAddresses)
        .HasForeignKey(e => e.BusinessEntityID);

            modelBuilder.Entity<PersonPhone>()
        .HasOne(pp => pp.Person)
        .WithMany(p => p.Phones)
        .HasForeignKey(pp => pp.BusinessEntityID);

            modelBuilder.Entity<BusinessEntityAddress>()
                   .HasOne(bea => bea.Person)
                   .WithMany(p => p.BusinessEntityAddresses)
                   .HasForeignKey(bea => bea.BusinessEntityID);

            modelBuilder.Entity<BusinessEntityAddress>()
                .HasOne(bea => bea.Address)
                .WithMany(a => a.BusinessEntityAddresses)
                .HasForeignKey(bea => bea.AddressID);

            // BusinessEntityAddress (many-to-many)
            modelBuilder.Entity<BusinessEntityAddress>()
                .HasKey(bea => new { bea.BusinessEntityID, bea.AddressID });
        }
    }
    }
 