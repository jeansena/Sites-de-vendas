using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AppModel.Models;

namespace AppModel.Data;

public class AppModelContext : DbContext
{
    public AppModelContext (DbContextOptions<AppModelContext> options)
        : base(options)
    {
       // Database.EnsureCreated();
    }

    /// <summary>
    ///
    ///conexao com o bancode dado
    ///criaçao das tabelas
    public DbSet<Department> Department { get; set; }
    public DbSet<Seller> Seller{ get; set; }
    public DbSet<SalesRecord> SalesRecord { get; set; }
}
