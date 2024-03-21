using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ClinicaVeterinaria.Models
{
    public partial class DBContext : DbContext
    {
        public DBContext()
            : base("name=DBContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<Animali> Animali { get; set; }
        public virtual DbSet<DisposizioneMedicinali> DisposizioneMedicinali { get; set; }
        public virtual DbSet<Prodotti> Prodotti { get; set; }
        public virtual DbSet<Ricoveri> Ricoveri { get; set; }
        public virtual DbSet<RimborsiRicoveri> RimborsiRicoveri { get; set; }
        public virtual DbSet<Utenti> Utenti { get; set; }
        public virtual DbSet<Vendite> Vendite { get; set; }
        public virtual DbSet<Visite> Visite { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Animali>()
                .HasMany(e => e.Ricoveri)
                .WithRequired(e => e.Animali)
                .HasForeignKey(e => e.IDAnimale)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Animali>()
                .HasMany(e => e.Visite)
                .WithRequired(e => e.Animali)
                .HasForeignKey(e => e.IDAnimale)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Prodotti>()
                .HasMany(e => e.DisposizioneMedicinali)
                .WithRequired(e => e.Prodotti)
                .HasForeignKey(e => e.IDProdotto)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Prodotti>()
                .HasMany(e => e.Vendite)
                .WithRequired(e => e.Prodotti)
                .HasForeignKey(e => e.IDProdotto)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ricoveri>()
                .HasMany(e => e.RimborsiRicoveri)
                .WithRequired(e => e.Ricoveri)
                .HasForeignKey(e => e.IDRicovero)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RimborsiRicoveri>()
                .Property(e => e.Importo)
                .HasPrecision(10, 2);
        }
    }
}
