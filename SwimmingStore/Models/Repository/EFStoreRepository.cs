using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwimmingStore.Models.Repository
{
    public class EFStoreRepository : IStoreRepository
    {
        private StoreDbContext _context;
        public EFStoreRepository(StoreDbContext context)
        {
            _context = context;
        }

        public IQueryable<Product> Products => _context.Products;
    }
}
