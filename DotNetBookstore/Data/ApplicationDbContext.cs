using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DotNetBookstore.Models;

namespace DotNetBookstore.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        // DbSets for for all entities - these represent the tables in the database

        public DbSet<Category> Categories

        {

            get; set;

        } = default!;

        public DbSet<Book> Books

        {

            get; set;

        } = default!;

        public DbSet<CartItem> CartItems

        {

            get; set;

        } = default!;

        public DbSet<Order> Orders

        {

            get; set;

        } = default!;

        public DbSet<OrderDetail> OrderDetails

        {

            get; set;

        } = default!;


    }
}
