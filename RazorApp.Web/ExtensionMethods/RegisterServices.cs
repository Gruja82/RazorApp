using Microsoft.EntityFrameworkCore;
using RazorApp.Data.Database;
using RazorApp.Data.Entities;
using RazorApp.Models.Dtos;
using RazorApp.Services.Repositories.Categories;
using RazorApp.Services.Repositories.Customers;
using RazorApp.Services.Repositories.Materials;
using RazorApp.Services.Repositories.Orders;
using RazorApp.Services.Repositories.Productions;
using RazorApp.Services.Repositories.Products;
using RazorApp.Services.Repositories.Purchases;
using RazorApp.Services.Repositories.Suppliers;
using RazorApp.Services.Repositories.UnitOfWork;

namespace RazorApp.Web.ExtensionMethods
{
    public static class RegisterServices
    {
        public static void AddServicesToContainer(this WebApplicationBuilder builder, string connString)
        {
            builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(connString));

            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            builder.Services.AddScoped<IMaterialRepository, MaterialRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IProductionRepository, ProductionRepository>();
            builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
            builder.Services.AddScoped<IPurchaseRepository, PurchaseRepository>();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
