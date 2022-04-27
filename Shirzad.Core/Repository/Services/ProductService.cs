using Microsoft.EntityFrameworkCore;
using Shirzad.Core.Repository.Interfaces;
using Shirzad.DataLayer.Context;
using Shirzad.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shirzad.Core.Repository.Services
{
    public class ProductService : IProductRepository
    {
        private readonly ApplicationContext _context;
        public ProductService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<ProductGallery> GetGalleryByProductId(int productId)
        {
            return await _context.ProductGalleries.Where(x => x.ProductId == productId).SingleOrDefaultAsync();
        }

        public async Task<bool> GetProductByProductNameAsync(string productName, int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x=> x.Name==productName);
            if(product != null && product.Id != id) return true;
            return false;
        }
    }
}
