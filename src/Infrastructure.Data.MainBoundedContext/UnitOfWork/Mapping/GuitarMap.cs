using Domain.MainBoundedContext.Aggregates.InventoryAgg;
using FluentNHibernate.Mapping;
using Infrastructure.Data.Seedwork;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.MainBoundedContext.UnitOfWork.Mapping
{
    public class GuitarMap : VersionedClassMap<Guitar>
    {
        public GuitarMap()
        {
            Schema("Store");
            Id(x => x.Id).CustomType<Guid>();
            Map(x => x.Type);

            HasMany(x => x.Inventory)
                .Access.ReadOnlyPropertyThroughCamelCaseField(Prefix.Underscore)
                .Inverse()
                .Cascade.All();
        }
    }
}
