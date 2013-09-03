using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainBoundedContext.DTO
{
    public class InventoryListDTO
    {
        public enum Filds {Id, Type, Builder, Model, QOH, Cost, Price, Received};
        public virtual Guid Id { get; set; }
        public virtual string Type { get; set; }
        public virtual string Builder { get; set; }
        public virtual string Model { get; set; }
        public virtual int? QOH { get; set; }
        public virtual decimal? Cost { get; set; }
        public virtual decimal? Price { get; set; }
        public virtual DateTime? Received { get; set; }
    }
}
