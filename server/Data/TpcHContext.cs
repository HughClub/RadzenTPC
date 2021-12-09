using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;

using RadzenDb.Models.TpcH;

namespace RadzenDb.Data
{
  public partial class TpcHContext : Microsoft.EntityFrameworkCore.DbContext
  {
    public TpcHContext(DbContextOptions<TpcHContext> options):base(options)
    {
    }

    public TpcHContext()
    {
    }

    partial void OnModelBuilding(ModelBuilder builder);

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<RadzenDb.Models.TpcH.Lineitem>().HasKey(table => new {
          table.l_orderkey, table.l_linenumber
        });
        builder.Entity<RadzenDb.Models.TpcH.Partsupp>().HasKey(table => new {
          table.ps_partkey, table.ps_suppkey
        });
        builder.Entity<RadzenDb.Models.TpcH.Customer>()
              .HasOne(i => i.Nation)
              .WithMany(i => i.Customers)
              .HasForeignKey(i => i.c_nationkey)
              .HasPrincipalKey(i => i.n_nationkey);
        builder.Entity<RadzenDb.Models.TpcH.Lineitem>()
              .HasOne(i => i.Order)
              .WithMany(i => i.Lineitems)
              .HasForeignKey(i => i.l_orderkey)
              .HasPrincipalKey(i => i.o_orderkey);
        builder.Entity<RadzenDb.Models.TpcH.Lineitem>()
              .HasOne(i => i.Partsupp)
              .WithMany(i => i.Lineitems)
              .HasForeignKey(i => new { i.l_partkey, i.l_suppkey })
              .HasPrincipalKey(i => new { i.ps_partkey, i.ps_suppkey });
        builder.Entity<RadzenDb.Models.TpcH.Nation>()
              .HasOne(i => i.Region)
              .WithMany(i => i.Nations)
              .HasForeignKey(i => i.n_regionkey)
              .HasPrincipalKey(i => i.r_regionkey);
        builder.Entity<RadzenDb.Models.TpcH.Order>()
              .HasOne(i => i.Customer)
              .WithMany(i => i.Orders)
              .HasForeignKey(i => i.o_custkey)
              .HasPrincipalKey(i => i.c_custkey);
        builder.Entity<RadzenDb.Models.TpcH.Partsupp>()
              .HasOne(i => i.Part)
              .WithMany(i => i.Partsupps)
              .HasForeignKey(i => i.ps_partkey)
              .HasPrincipalKey(i => i.p_partkey);
        builder.Entity<RadzenDb.Models.TpcH.Partsupp>()
              .HasOne(i => i.Supplier)
              .WithMany(i => i.Partsupps)
              .HasForeignKey(i => i.ps_suppkey)
              .HasPrincipalKey(i => i.s_suppkey);
        builder.Entity<RadzenDb.Models.TpcH.Supplier>()
              .HasOne(i => i.Nation)
              .WithMany(i => i.Suppliers)
              .HasForeignKey(i => i.s_nationkey)
              .HasPrincipalKey(i => i.n_nationkey);

        this.OnModelBuilding(builder);
    }


    public DbSet<RadzenDb.Models.TpcH.Customer> Customers
    {
      get;
      set;
    }

    public DbSet<RadzenDb.Models.TpcH.Lineitem> Lineitems
    {
      get;
      set;
    }

    public DbSet<RadzenDb.Models.TpcH.Nation> Nations
    {
      get;
      set;
    }

    public DbSet<RadzenDb.Models.TpcH.Order> Orders
    {
      get;
      set;
    }

    public DbSet<RadzenDb.Models.TpcH.Part> Parts
    {
      get;
      set;
    }

    public DbSet<RadzenDb.Models.TpcH.Partsupp> Partsupps
    {
      get;
      set;
    }

    public DbSet<RadzenDb.Models.TpcH.Region> Regions
    {
      get;
      set;
    }

    public DbSet<RadzenDb.Models.TpcH.Supplier> Suppliers
    {
      get;
      set;
    }
  }
}
