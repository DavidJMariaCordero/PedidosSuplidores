using Microsoft.EntityFrameworkCore;
using PedidosSuplidores.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace PedidosSuplidores.DAL
{
    public class Contexto : DbContext
    {
        public DbSet<Ordenes> Ordenes { get; set; }
        public DbSet<Productos> Productos { get; set; }
        public DbSet<Suplidores> Suplidores { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite(@"Data Source = Data/Compras.db");
        }
        
        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Productos>().HasData(new Productos() { Descripcion = "Papas fritas", Costo = 225 });
            modelBuilder.Entity<Productos>().HasData(new Productos() { Descripcion = "Salami Super Especial", Costo = 300 });
            modelBuilder.Entity<Productos>().HasData(new Productos() { Descripcion = "Greey Goose Vodka", Costo = 1200 });
            modelBuilder.Entity<Productos>().HasData(new Productos() { Descripcion = "Cheetos", Costo = 75 });
            modelBuilder.Entity<Productos>().HasData(new Productos() { Descripcion = "Takis", Costo = 20 });

            modelBuilder.Entity<Suplidores>().HasData(new Suplidores() { Nombres = "Vocatus"});
            modelBuilder.Entity<Suplidores>().HasData(new Suplidores() { Nombres = "Induveca" });
            modelBuilder.Entity<Suplidores>().HasData(new Suplidores() { Nombres = "FritoLay" });

        }*/
    }
}
