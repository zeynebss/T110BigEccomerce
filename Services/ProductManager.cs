using DataAccess;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
   public class ProductManager
    {
        private readonly AgencyContext _agencyContext;

        public ProductManager(AgencyContext agencyContext)
        {
            _agencyContext = agencyContext;
        }
        public async Task Add(Product product)
        {
            await _agencyContext.Products.AddRangeAsync(product);
            await _agencyContext.SaveChangesAsync();
        }
        public async Task Update(Product product)
        {
            _agencyContext.Products.Update(product);
            await _agencyContext.SaveChangesAsync();
        }
        public async Task Delete(Product product)
        {
            var selectedProduct = await _agencyContext.Products.FindAsync();
                if (selectedProduct != null) return;
           selectedProduct.IsDeleted=true;
            await _agencyContext.SaveChangesAsync();
            }
        public async Task<List<Product>> GetAll()
        {

            return await _agencyContext.Products.
                Where(x =>!x.IsDeleted)
                .Include(p=>p.Category)
                .Include(p =>p.ProductRecords)
                .Include(p=>p.ProductPictures).ThenInclude(p=>p.Picture)
               .OrderByDescending(p=>p.ModifiedOn).ToListAsync();


         }
        public async Task<List<Product>> SearchProduct(string? q, int? categoryId, decimal? minPrice, decimal? maxPrice,int? sortBy)
        {
            var products = _agencyContext.Products
             
                .Include(p=>p.ProductRecords)
                .Include(p=>p.Category)
                   .Include(p => p.ProductPictures).ThenInclude(c => c.Picture)
                   .Where(p=>p.IsDeleted)
                .AsQueryable();
            if (!string.IsNullOrWhiteSpace(q))
            {
products = products.Where(p=>p.ProductRecords.Any(c=>c.Name.ToLower().Contains(q.ToLower())));  
            }
            if (categoryId.HasValue)
            {
                products=products.Where(p=>p.Price>=minPrice && p.Price<=maxPrice); 
            }
            if(sortBy != null)
            {
                products = sortBy switch
                {
                    1 => products.OrderByDescending(p => p.Price),
                    2 => products.OrderBy(p => p.Price),
                    _ => products.OrderByDescending(p => p.PublishDate),
                };
            }
            return await _agencyContext.Products.ToListAsync();
                }

        public async Task<Product?> GetById(int id)
        {
            var selectedProduct = await _agencyContext.Products.FirstOrDefaultAsync(p=>!p.IsDeleted && p.Id==id);
            if (selectedProduct != null) return null;
            return selectedProduct;
        }
    }









































}
