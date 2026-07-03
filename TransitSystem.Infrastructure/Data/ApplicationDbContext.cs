using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TransitSystem.Core.Domain.Entities;
using TransitSystem.Core.Domain.Cards;

namespace TransitSystem.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<UserAccount> Accounts { get; set; }
        public DbSet<DigitalCard> Cards { get; set; }
        public DbSet<RechargeTransaction> RechargeTransactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1. Configuraciones de llaves primarias estándar
            modelBuilder.Entity<UserAccount>().HasKey(a => a.AccountId);
            modelBuilder.Entity<RechargeTransaction>().HasKey(t => t.TransactionId);

            // 2. Configuración de la llave primaria de la tarjeta
            modelBuilder.Entity<DigitalCard>().HasKey(c => c.TokenId);

            // 3. MAPEO DE HERENCIA NATIVA (TPH)
            modelBuilder.Entity<DigitalCard>()
                .HasDiscriminator(c => c.CardType) // Usamos tu propiedad formal como columna discriminadora
                .HasValue<Linea1GeneralCard>("Linea1General")
                .HasValue<MetropolitanoGeneralCard>("MetropolitanoGeneral")
                .HasValue<MetropolitanoUniversitarioCard>("MetropolitanoUniversitario")
                .HasValue<VirtualWalletCard>("VirtualWallet");
        }
    }
}