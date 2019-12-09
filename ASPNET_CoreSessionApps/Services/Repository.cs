using ASPNET_CoreSessionApps.Models;
using System.Collections.Generic;
using System.Linq;

namespace ASPNET_CoreSessionApps.Services
{
    public interface IRepository<T, U> where T : class
    {
        IEnumerable<T> Get();
        T Get(U id);
        bool Create(T data);
        bool Update(U id, T data);
        bool Delete(U id);
    }
    public class CategoryRepository : IRepository<Category, int>
    {
         SuperMarketContext ctx;
        public CategoryRepository(SuperMarketContext c)
        {
            ctx = c;
        }
        public bool Create(Category data)
        {
            int res = 0;
            bool isSuccess = false;
            ctx.Category.Add(data);
            res = ctx.SaveChanges();
            if (res >= 0) {
                isSuccess =  true;
            }
            return isSuccess;
        }

        public bool Delete(int id)
        {
            bool isSuccess = false;
            var cat = ctx.Category.Find(id);
            if (cat != null) {
                ctx.Category.Remove(cat);
                ctx.SaveChanges();
                isSuccess = true;
            }
            return isSuccess;
        }

        public IEnumerable<Category> Get()
        {
            var cats = ctx.Category.ToList();
            return cats;
        }

        public Category Get(int id)
        {
            var cat = ctx.Category.Find(id);
            return cat;
        }

        public bool Update(int id, Category data)
        {
            bool isSuccess = false;
            var cat = ctx.Category.Find(id);
            if (cat != null) {
                cat.CategoryName = data.CategoryName;
                ctx.SaveChanges();
            }
            return isSuccess;
        }
    }

    public class ProductRepository : IRepository<Product, int>
    {
        SuperMarketContext ctx;

        public ProductRepository(SuperMarketContext c)
        {
            ctx = c;
        }
        public bool Create(Product data)
        {
            bool isSuccess = false;
            int res = 0;
            ctx.Product.Add(data);
            res = ctx.SaveChanges();
            if (res > 0) {
                isSuccess = true;
            }  
            return isSuccess;
        }

        public bool Delete(int id)
        {
            bool isSuccess = false;
            var prd = ctx.Product.Find(id);
            if (ctx != null)
            {
                ctx.Product.Remove(prd);
                ctx.SaveChanges();
                isSuccess = true;
            }
            return isSuccess;
        }

        public IEnumerable<Product> Get()
        {
            var prds = ctx.Product.ToList();
            return prds;
        }

        public Product Get(int id)
        {
            var prd = ctx.Product.Find(id);
            return prd;
        }

        public bool Update(int id, Product data)
        {
            bool isSuccess = false;
            var prd = ctx.Product.Find(id);
            if (prd != null)
            {
                prd.ProductName = data.ProductName;
                prd.UnitPrice = data.UnitPrice;
                prd.CategoryId = data.CategoryId;
                ctx.SaveChanges();
                isSuccess = true;
            }
            return isSuccess;
        }
    }

    public interface IBill
    {
        bool GenerateBill(BillMaster bill, BillDetails []details);
    }

    public class BillGenerator : IBill
    {
        SuperMarketContext ctx;
        public BillGenerator(SuperMarketContext c)
        {
            ctx = c;
        }
        public bool GenerateBill(BillMaster bill, BillDetails []details)
        {
            int res = 0;
            bool isSuccess = false;

            ctx.BillMaster.Add(bill);
            ctx.BillDetails.AddRange(details);
            res = ctx.SaveChanges();
            if (res > 0)
            {
                isSuccess = true;
            }
            return isSuccess;
        }
    }
}
