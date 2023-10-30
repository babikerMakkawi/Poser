using Poser.Core;
using Poser.Core.Interfaces;
using Poser.Core.Models.Products;
using Poser.EF.Repositories;

namespace Poser.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IBaseRepository<Poser.Core.Models.Products.Attribute> Attributes {  get; private set; }
        public IBaseRepository<AttributeValue> AttributeValues {  get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            Attributes = new BaseRepository<Core.Models.Products.Attribute>(context);
            AttributeValues = new BaseRepository<AttributeValue>(context);
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