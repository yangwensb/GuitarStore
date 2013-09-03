using Domain.Seedwork.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.MainBoundedContext.Aggregates.InventoryAgg
{
    public static class InventorySpecification 
    {
        public static ISpecification<Inventory> InventoryByModel(string model)
        {
            var specification = new TrueSpecification<Inventory>() as Specification<Inventory>;

            if(!String.IsNullOrEmpty(model))
            {
                
                specification &= new DirectSpecification<Inventory>(i => i.Model.ToLower().Contains(model.ToLower()));
            }

            return specification;
        }
    }
}
