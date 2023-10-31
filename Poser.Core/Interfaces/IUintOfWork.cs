namespace Poser.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<Models.Brand> Brands { get; }
        IBaseRepository<Models.Category> Categories { get; }
        IBaseRepository<Models.Orders.PaymentMethod> PaymentMethods { get; }
        IBaseRepository<Models.Customer> Customers { get; }

        IBaseRepository<Models.Products.Attribute> Attributes { get; }
        IBaseRepository<Models.Products.AttributeValue> AttributeValues { get; }

        IBaseRepository<Models.Products.Product> Products { get; }
        IBaseRepository<Models.Products.ProductStock> ProductStocks { get; }
        IBaseRepository<Models.Products.ProductAttribute> ProductAttributes { get; }
        

        IBaseRepository<Models.Orders.Order> Orders { get; }
        IBaseRepository<Models.Orders.OrderProduct> OrderProducts { get; }

        int complete();
    }
}
