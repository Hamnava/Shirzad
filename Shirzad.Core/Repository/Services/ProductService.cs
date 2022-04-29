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


        public List<Product> Search(string text, List<int> categoryid, int sort = 1)
        {
            IQueryable<Product> products =   _context.Products.Where(x => x.Name.Contains(text));

            switch (sort)
            {
                case 1:
                    products = products.OrderByDescending(p => p.BestSaller);
                    break;
                case 2:
                    products = products.OrderByDescending(p => p.IsNew);
                    break;
                case 3:
                    products = products.OrderBy(p => p.Price);
                    break;
                case 4: 
                    products = products.OrderByDescending(p=> p.Price);
                    break;
            }

            if(categoryid.Count() > 0)
            {
                products = products.Where(c => categoryid.Contains(c.CategoryId));
            }

            var query = (from p in products
                         select new
                         {
                             id = p.Id,
                             name = p.Name,
                             price = p.Price,
                             isNew = p.IsNew,
                             image = p.PhotoUrl
                         });

            List<Product> listProducts = new List<Product>();
            foreach (var product in products)
            {
                listProducts.Add(new Product
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    IsNew = product.IsNew,
                    PhotoUrl = product.PhotoUrl
                });
            }

            return listProducts;
        }

        
    }
}
