using E_Commence.DataAccess.Data;
using E_Commence.DataAccess.Repository.IRepository;
using E_Commence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commence.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
       

        public void Update(Product product)
        {
            var objFromDb = _context.Products.FirstOrDefault(u=>u.Id == product.Id);
            if (objFromDb != null)
            {
                objFromDb.Title = product.Title;
                objFromDb.Description = product.Description;
                objFromDb.Price = product.Price;
                objFromDb.CategoryId = product.CategoryId;
                objFromDb.ISBN = product.ISBN;
                objFromDb.Author = product.Author;
                objFromDb.Price100 = product.Price100;
                objFromDb.Price50 = product.Price50;
                objFromDb.ListPrice = product.ListPrice;
                if(product.ImgUrl != null) { 
                    objFromDb.ImgUrl = product.ImgUrl;
                }
            }
        }
    }
}
