namespace Poser.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<Models.Products.Attribute> Attributes { get; }
        IBaseRepository<Models.Products.AttributeValue> AttributeValues { get; }
        int complete();
    }
}
