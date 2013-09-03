using Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.MainBoundedContext.Aggregates.InventoryAgg
{
    /// <summary>
    /// Quantity of each guitar type in stock, plus other details
    /// </summary>
    public class Inventory : Entity,  IVersionedObject
    {
        public Inventory() { }
        public virtual Guitar Type { get; set; }
        public virtual string Builder { get; set; }
        public virtual string Model { get; set; }
        public virtual int? QOH { get; set; }
        public virtual decimal? Cost { get; set; }
        public virtual decimal? Price { get; set; }
        public virtual DateTime? Received { get; set; }

        public virtual byte[] Version { get; set; }
    }
}
