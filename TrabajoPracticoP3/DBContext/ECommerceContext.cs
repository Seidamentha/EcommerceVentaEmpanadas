using Microsoft.EntityFrameworkCore;
using TrabajoPracticoP3.Data.Entities;
using TrabajoPracticoP3.Data.Enum;

namespace TrabajoPracticoP3.DBContext
{
    public class ECommerceContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<SaleOrderLine> SaleOrderLines { get; set; }  // Añadido aquí

        public ECommerceContext(DbContextOptions<ECommerceContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Herencia con Discriminator
            modelBuilder.Entity<User>()
                .Property(u => u.UserType)
                .HasConversion<string>();



                modelBuilder.Entity<User>()
                .HasDiscriminator<string>("UserType")
                .HasValue<Admin>("Admin")
                .HasValue<Client>("Client")
                .HasValue<Seller>("Seller");

            // Insertamos datos iniciales en la base de datos
            modelBuilder.Entity<Admin>().HasData(new Admin
            {
                Id = 1,
                Name = "Amnetha",
                SurName = "Seide",
                Email = "amnetha@gmail.com",
                UserName = "Tatie",
                UserType = UserType.Admin.ToString(),
                Password = "987654" // ⚠️ Reemplazar con un hash en la lógica de negocio
            });

            modelBuilder.Entity<Client>().HasData(
                new Client
                {
                    Id = 2,
                    Name = "Sofia",
                    SurName = "Mazzurco",
                    Email = "sofi@gmail.com",
                    UserName = "Tutti",
                    UserType = UserType.Client.ToString(),
                    Password = "123321", // ⚠️ Reemplazar con un hash
                    PhoneNumber = "3415123212",
                    Adress = "Pellegrini 211"
                },
                new Client
                {
                    Id = 3,
                    Name = "Guido",
                    SurName = "Montenegro",
                    Email = "guido@gmail.com",
                    UserName = "Monzón",
                    UserType = UserType.Client.ToString(),
                    Password = "554466", // ⚠️ Reemplazar con un hash
                    PhoneNumber = "3415123333",
                    Adress = "Mendoza 211"
                }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Empanada de Verduras",
                    Price = 300
                },
                new Product
                {
                    Id = 2,
                    Name = "Empanada de Jamón y Queso",
                    Price = 500
                },
                new Product
                {
                    Id = 3,
                    Name = "Empanada de Carne",
                    Price = 400
                }
            );

            // Relaciones

            // Relación 1:N entre Order y SaleOrderLine
            modelBuilder.Entity<SaleOrderLine>()
                .HasOne(sol => sol.Order)
                .WithMany(o => o.SaleOrderLines)
                .HasForeignKey(sol => sol.OrderId)
                .OnDelete(DeleteBehavior.Cascade); // Para que se eliminen en cascada si la orden se borra

            // Relación 1:N entre Product y SaleOrderLine
            modelBuilder.Entity<SaleOrderLine>()
                .HasOne(sol => sol.Product)
                .WithMany(p => p.SaleOrderLines)
                .HasForeignKey(sol => sol.ProductId)
                .OnDelete(DeleteBehavior.Restrict); // Para evitar la eliminación accidental de productos en uso

            // Relación 1:N entre Client y Order
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Client)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.ClientId)
                .OnDelete(DeleteBehavior.Cascade); // Si se elimina un cliente, también sus órdenes

            base.OnModelCreating(modelBuilder);
        }
    }
}

