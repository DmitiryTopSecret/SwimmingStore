using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwimmingStore.Models.Repository
{
    public interface IStoreRepository
    {
        IQueryable<Product> Products { get; }
    }
}
