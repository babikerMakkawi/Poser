using Poser.Core;
using Poser.Core.Models;
using Poser.Core.Models.Products;
using Poser.Core.Interfaces;
using Poser.EF.Repositories;
using Poser.Core.Models.Orders;

namespace Poser.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IBaseRepository<Brand> Brands { get; private set;}
        public IBaseRepository<Category> Categories { get; private set; }
        public IBaseRepository<PaymentMethod> PaymentMethods { get; private set; }
        public IBaseRepository<Customer> Customers { get; private set; }

        public IBaseRepository<Core.Models.Products.Attribute> Attributes { get; private set; }
        public IBaseRepository<AttributeValue> AttributeValues { get; private set; }

        public IBaseRepository<Product> Products { get; private set; }
        public IBaseRepository<ProductStock> ProductStocks { get; private set; }
        public IBaseRepository<ProductAttribute> ProductAttributes { get; private set; }


        public IBaseRepository<Order> Orders { get; private set; }
        public IBaseRepository<OrderProduct> OrderProducts { get; private set; }


        public UnitOfWork(ApplicationDbContext context)
        {
            _context          = context;

            Brands            = new BaseRepository<Brand>(context);
            Categories        = new BaseRepository<Category>(context);
            PaymentMethods    = new BaseRepository<PaymentMethod>(context);
            Customers         = new BaseRepository<Customer>(context);

            Attributes        = new BaseRepository<Core.Models.Products.Attribute>(context);
            AttributeValues   = new BaseRepository<AttributeValue>(context);

            Products          = new BaseRepository<Product>(context);
            ProductStocks     = new BaseRepository<ProductStock>(context);
            ProductAttributes = new BaseRepository<ProductAttribute>(context);

            Orders            = new BaseRepository<Order>(context);
            OrderProducts     = new BaseRepository<OrderProduct>(context);

        }

        public int complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}