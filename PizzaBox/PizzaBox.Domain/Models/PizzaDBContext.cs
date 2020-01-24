using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PizzaBox.Domain.Models
{
    public partial class PizzaDBContext : DbContext
    {
        public PizzaDBContext()
        {
        }

        public PizzaDBContext(DbContextOptions<PizzaDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<OrdersPizzaInfo> OrdersPizzaInfo { get; set; }
        public virtual DbSet<OrdersUserInfo> OrdersUserInfo { get; set; }
        public virtual DbSet<Pizzas> Pizzas { get; set; }
        public virtual DbSet<PresetPizzas> PresetPizzas { get; set; }
        public virtual DbSet<StoreInfo> StoreInfo { get; set; }
        public virtual DbSet<StoreOrdersInfo> StoreOrdersInfo { get; set; }
        public virtual DbSet<StorePresetPizzas> StorePresetPizzas { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-6SDGA9L\\SQLEXPRESS;Database=PizzaDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrdersPizzaInfo>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.PizzaId })
                    .HasName("pk_order_Pizza_Info");

                entity.ToTable("OrdersPizzaInfo", "PizzaBox");

                entity.Property(e => e.OrderId).HasColumnName("orderId");

                entity.Property(e => e.PizzaId).HasColumnName("pizzaId");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrdersPizzaInfo)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Orders_User");

                entity.HasOne(d => d.Pizza)
                    .WithMany(p => p.OrdersPizzaInfo)
                    .HasForeignKey(d => d.PizzaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Pizza");
            });

            modelBuilder.Entity<OrdersUserInfo>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("pk_order_User_Info");

                entity.ToTable("OrdersUserInfo", "PizzaBox");

                entity.Property(e => e.OrderId).HasColumnName("orderId");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.OrderDateTime)
                    .HasColumnName("orderDateTime")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.EmailNavigation)
                    .WithMany(p => p.OrdersUserInfo)
                    .HasForeignKey(d => d.Email)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_users");
            });

            modelBuilder.Entity<Pizzas>(entity =>
            {
                entity.HasKey(e => e.PizzaId)
                    .HasName("pk_pizzas");

                entity.ToTable("Pizzas", "PizzaBox");

                entity.Property(e => e.PizzaId).HasColumnName("pizzaId");

                entity.Property(e => e.CheeseAmount)
                    .IsRequired()
                    .HasColumnName("cheeseAmount")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Crust)
                    .IsRequired()
                    .HasColumnName("crust")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CrustFlavor)
                    .HasColumnName("crustFlavor")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("money");

                entity.Property(e => e.Sauce)
                    .IsRequired()
                    .HasColumnName("sauce")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SauceAmount)
                    .IsRequired()
                    .HasColumnName("sauceAmount")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Size)
                    .IsRequired()
                    .HasColumnName("size")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Topping1)
                    .HasColumnName("topping1")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Topping2)
                    .HasColumnName("topping2")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Topping3)
                    .HasColumnName("topping3")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PresetPizzas>(entity =>
            {
                entity.HasKey(e => e.PizzaName)
                    .HasName("pk_preset_pizzas");

                entity.ToTable("PresetPizzas", "PizzaBox");

                entity.HasIndex(e => new { e.Size, e.Crust, e.CrustFlavor, e.Sauce, e.SauceAmount, e.CheeseAmount, e.Topping1, e.Topping2, e.Topping3 })
                    .HasName("unique_pizzas")
                    .IsUnique();

                entity.Property(e => e.PizzaName)
                    .HasColumnName("pizzaName")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CheeseAmount)
                    .IsRequired()
                    .HasColumnName("cheeseAmount")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Crust)
                    .IsRequired()
                    .HasColumnName("crust")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CrustFlavor)
                    .HasColumnName("crustFlavor")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("money");

                entity.Property(e => e.Sauce)
                    .IsRequired()
                    .HasColumnName("sauce")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SauceAmount)
                    .IsRequired()
                    .HasColumnName("sauceAmount")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Size)
                    .IsRequired()
                    .HasColumnName("size")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Topping1)
                    .HasColumnName("topping1")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Topping2)
                    .HasColumnName("topping2")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Topping3)
                    .HasColumnName("topping3")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<StoreInfo>(entity =>
            {
                entity.HasKey(e => e.StoreId)
                    .HasName("pk_store");

                entity.ToTable("StoreInfo", "PizzaBox");

                entity.HasIndex(e => new { e.StoreName, e.Address, e.City, e.State, e.ZipCode })
                    .HasName("unique_stores")
                    .IsUnique();

                entity.Property(e => e.StoreId).HasColumnName("storeId");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasColumnName("city")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasColumnName("state")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.StoreName)
                    .IsRequired()
                    .HasColumnName("storeName")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.StorePrice)
                    .HasColumnName("storePrice")
                    .HasColumnType("money");

                entity.Property(e => e.ZipCode)
                    .IsRequired()
                    .HasColumnName("zipCode")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<StoreOrdersInfo>(entity =>
            {
                entity.HasKey(e => new { e.StoreId, e.OrderId })
                    .HasName("pk_store_orders");

                entity.ToTable("StoreOrdersInfo", "PizzaBox");

                entity.Property(e => e.StoreId).HasColumnName("storeId");

                entity.Property(e => e.OrderId).HasColumnName("orderId");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.StoreOrdersInfo)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_orders");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.StoreOrdersInfo)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_store");
            });

            modelBuilder.Entity<StorePresetPizzas>(entity =>
            {
                entity.HasKey(e => new { e.StoreId, e.PizzaName })
                    .HasName("pk_store_preset");

                entity.ToTable("StorePresetPizzas", "PizzaBox");

                entity.Property(e => e.StoreId).HasColumnName("storeId");

                entity.Property(e => e.PizzaName)
                    .HasColumnName("pizzaName")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.PizzaNameNavigation)
                    .WithMany(p => p.StorePresetPizzas)
                    .HasForeignKey(d => d.PizzaName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_preset_pizza");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.StorePresetPizzas)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_store_info");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.Email)
                    .HasName("pk_users");

                entity.ToTable("Users", "PizzaBox");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
