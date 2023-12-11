    using Microsoft.EntityFrameworkCore;
using TrabajoPracticoP3.Data.Entities;
using TrabajoPracticoP3.Data.Enum;

namespace TrabajoPracticoP3.DBContext
{
    public class ECommerceContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        public DbSet<SaleOrderLine> SaleOrderLines { get; set; }
        public ECommerceContext(DbContextOptions<ECommerceContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasDiscriminator(u => u.UserType);


            modelBuilder.Entity<Admin>().HasData(new Admin
            {
                Id = 1,
                Name = "Amentha",
                SurName = "Seide",
                Email = "Amentha@gmail.com",
                UserName = "Tatie",
                UserType = UserType.Admin,
                Password = "987654"

            });

            modelBuilder.Entity<Client>().HasData(
                new Client
                {
                    Id = 2,
                    Name = "Sofia",
                    SurName = "Mazzurco",
                    Email = "Sofi@gmail.com",
                    UserName = "Tutti",
                    UserType = UserType.Client,
                    Password = "123321",
                    PhoneNumber = "3415123212",
                    Adress = "Pellegrini 211"
                },
                new Client
                {
                    Id = 3,
                    Name = "Guido",
                    SurName = "Montenegro",
                    Email = "Guido@gmail.com",
                    UserName = "Monzón",
                    UserType = UserType.Client,
                    Password = "554466",
                    PhoneNumber = "3415123333",
                    Adress = "Mendoza 211"
                }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    IdProduct = 1,
                    NameProduct = "Empanada de verduras",
                    Price = 300
                },
                new Product
                {
                    IdProduct= 2,
                    NameProduct= "Emoanada de JAmón y Queso",
                    Price = 500
                },
                new Product
                {
                    IdProduct = 3,
                    NameProduct = "Empanada de Carne",
                    Price = 400
                }
            );

            modelBuilder.Entity<User>().HasDiscriminator(u => u.UserType);

            modelBuilder.Entity<SaleOrderLine>()
                .HasOne(op => op.Product)
                .WithMany()
                .HasForeignKey(op => op.ProductId);

            modelBuilder.Entity<SaleOrderLine>()
                .HasOne(sol => sol.Order)
                .WithOne();

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Client)
                .WithMany()
                .HasForeignKey(o => o.ClientId);

            base.OnModelCreating(modelBuilder);
        }
    }
}


