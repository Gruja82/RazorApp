using RazorApp.Data.Entities;
using RazorApp.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorApp.Services.Extensions
{
    public static class ExtensionMethods
    {
        public static string StoreImage(this BaseDto baseDto, string imagesFolder)
        {
            string? imageFileName = string.Empty;

            if (baseDto.Image != null)
            {
                StringBuilder sb = new();

                sb.Append(Guid.NewGuid().ToString().Substring(0, 10));
                sb.Append("_");
                sb.Append(baseDto.Image.FileName);

                imageFileName = sb.ToString();

                string filePath = Path.Combine(imagesFolder, imageFileName);

                using var fileStream = new FileStream(filePath, FileMode.Create);

                baseDto.Image.CopyTo(fileStream);
            }

            return imageFileName;
        }

        public static CategoryDto ConvertToDto(this Category category)
        {
            CategoryDto categoryDto = new();

            categoryDto.Id = category.Id;
            categoryDto.Code = category.Code;
            categoryDto.Name = category.Name;
            categoryDto.Description = category.Description;

            return categoryDto;
        }

        public static CustomerDto ConvertToDto(this Customer customer)
        {
            CustomerDto customerDto = new();

            customerDto.Id = customer.Id;
            customerDto.Code = customer.Code;
            customerDto.Name = customer.Name;
            customerDto.Contact = customer.Contact;
            customerDto.Address = customer.Address;
            customerDto.City = customer.City;
            customerDto.Postal = customer.Postal;
            customerDto.Phone = customer.Phone;
            customerDto.Email = customer.Email;
            customerDto.Web = customer.Web;
            customerDto.ImageUrl = customer.ImageUrl;

            return customerDto;
        }

        public static SupplierDto ConvertToDto(this Supplier supplier)
        {
            SupplierDto supplierDto = new();

            supplierDto.Id = supplier.Id;
            supplierDto.Code = supplier.Code;
            supplierDto.Name = supplier.Name;
            supplierDto.Contact = supplier.Contact;
            supplierDto.Address = supplier.Address;
            supplierDto.City = supplier.City;
            supplierDto.Postal = supplier.Postal;
            supplierDto.Phone = supplier.Phone;
            supplierDto.Email = supplier.Email;
            supplierDto.Web = supplier.Web;
            supplierDto.ImageUrl = supplier.ImageUrl;

            return supplierDto;
        }

        public static MaterialDto ConvertToDto(this Material material)
        {
            MaterialDto materialDto = new();

            materialDto.Id = material.Id;
            materialDto.Code = material.Code;
            materialDto.Name = material.Name;
            materialDto.CategoryName = material.Category.Name;
            materialDto.Qty = material.Qty;
            materialDto.Price = material.Price;
            materialDto.ImageUrl = material.ImageUrl;

            return materialDto;
        }

        public static ProductDetailDto ConvertToDto(this ProductDetail productDetail)
        {
            ProductDetailDto productDetailDto = new();

            productDetailDto.Id = productDetail.Id;
            productDetailDto.ProductName = productDetail.Product.Name;
            productDetailDto.MaterialName = productDetail.Material.Name;
            productDetailDto.Qty = productDetail.Qty;

            return productDetailDto;
        }

        public static ProductDto ConvertToDto(this Product product)
        {
            ProductDto productDto = new();

            productDto.Id = product.Id;
            productDto.Code = product.Code;
            productDto.Name = product.Name;
            productDto.CategoryName = product.Category.Name;
            productDto.Qty = product.Qty;
            productDto.Price = product.Price;
            productDto.ImageUrl = product.ImageUrl;

            foreach (var productDetail in product.ProductDetails)
            {
                productDto.ProductDetailDtos.Add(productDetail.ConvertToDto());
            }

            return productDto;
        }

        public static OrderDetailDto ConvertToDto(this OrderDetail orderDetail)
        {
            OrderDetailDto orderDetailDto = new();

            orderDetailDto.Id = orderDetail.Id;
            orderDetailDto.OrderCode = orderDetail.Order.Code;
            orderDetailDto.ProductName = orderDetail.Product.Name;
            orderDetailDto.Qty = orderDetail.Qty;

            return orderDetailDto;
        }

        public static OrderDto ConvertToDto(this Order order)
        {
            OrderDto orderDto = new();

            orderDto.Id = order.Id;
            orderDto.OrderCode = order.Code;
            orderDto.OrderDate = order.OrderDate;
            orderDto.CustomerName = order.Customer.Name;

            foreach (var orderDetail in order.OrderDetails)
            {
                orderDto.OrderDetailDtos.Add(orderDetail.ConvertToDto());
            }

            return orderDto;
        }

        public static ProductionDto ConvertToDto(this Production production)
        {
            ProductionDto productionDto = new();

            productionDto.Id = production.Id;
            productionDto.Code = production.Code;
            productionDto.ProductionDate = production.ProductionDate;
            productionDto.ProductName = production.Product.Name;
            productionDto.Qty = production.Qty;

            return productionDto;
        }

        public static PurchaseDetailDto ConvertToDto(this PurchaseDetail purchaseDetail)
        {
            PurchaseDetailDto purchaseDetailDto = new();

            purchaseDetailDto.Id = purchaseDetail.Id;
            purchaseDetailDto.PurchaseCode = purchaseDetail.Purchase.Code;
            purchaseDetailDto.MaterialName = purchaseDetail.Material.Name;
            purchaseDetailDto.Qty = purchaseDetail.Qty;

            return purchaseDetailDto;
        }

        public static PurchaseDto ConvertToDto(this Purchase purchase)
        {
            PurchaseDto purchaseDto = new();

            purchaseDto.Id = purchase.Id;
            purchaseDto.PurchaseCode = purchase.Code;
            purchaseDto.SupplierName = purchase.Supplier.Name;
            purchaseDto.PurchaseDate = purchase.PurchaseDate;

            foreach (var purchaseDetail in purchase.PurchaseDetails)
            {
                purchaseDto.PurchaseDetailDtos.Add(purchaseDetail.ConvertToDto());
            }

            return purchaseDto;
        }
    }
}
