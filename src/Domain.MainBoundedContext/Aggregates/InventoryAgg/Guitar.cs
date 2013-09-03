using Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.MainBoundedContext.Aggregates.InventoryAgg
{
    public class Guitar : ValueObject<Guitar>, IVersionedObject
    {
        private readonly IList<Inventory> _inventory = new List<Inventory>();

        #region Properties
        public virtual Guid Id { get; protected set; }
        public virtual string Type { get; set; }
        public virtual IList<Inventory> Inventory 
        {
            get { return _inventory; }
            protected set { throw new NotImplementedException(); }
        }
        public virtual byte[] Version { get; set; } 
        #endregion

        public Guitar()
        {
            _inventory = new List<Inventory>();
        }

        public virtual void AddInventory(Inventory inventory)
        {
            inventory.Type = this;
            Inventory.Add(inventory);
        }
    }
}
