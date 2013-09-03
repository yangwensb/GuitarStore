using Domain.MainBoundedContext.Aggregates.InventoryAgg;
using Infrastructure.Data.Seedwork;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.MainBoundedContext.UnitOfWork.Mapping
{
    public class InventoryMap : VersionedClassMap<Inventory>
    {
        public InventoryMap()
        {
            Schema("Store");
            Id(x => x.Id).CustomType<Guid>();
            References(x => x.Type, "TypeId");
            Map(x => x.Builder);
            Map(x => x.Model);
            Map(x => x.QOH);
            Map(x => x.Cost);
            Map(x => x.Price);
            Map(x => x.Received);
        }
    }
}
