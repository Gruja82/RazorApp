using RazorApp.Services.Repositories.Categories;
using RazorApp.Services.Repositories.Customers;
using RazorApp.Services.Repositories.Materials;
using RazorApp.Services.Repositories.Orders;
using RazorApp.Services.Repositories.Productions;
using RazorApp.Services.Repositories.Products;
using RazorApp.Services.Repositories.Purchases;
using RazorApp.Services.Repositories.Suppliers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorApp.Services.Repositories.UnitOfWork
{
    public interface IUnitOfWork:IDisposable
    {
        public ICategoryRepository CategoryRepository { get; }
        public ICustomerRepository CustomerRepository { get; }
        public IMaterialRepository MaterialRepository { get; }
        public IProductRepository ProductRepository { get; }
        public IOrderRepository OrderRepository { get; }
        public IProductionRepository ProductionRepository { get; }
        public ISupplierRepository SupplierRepository { get; }
        public IPurchaseRepository PurchaseRepository { get; }
        Task ConfirmChangesAsync();
    }
}
