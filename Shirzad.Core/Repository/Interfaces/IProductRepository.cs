using Shirzad.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shirzad.Core.Repository.Interfaces
{
    public interface IProductRepository
    {
        Task<bool> GetProductByProductNameAsync(string productName, int id);
        Task<ProductGallery> GetGalleryByProductId(int productId);
        
    }
}
