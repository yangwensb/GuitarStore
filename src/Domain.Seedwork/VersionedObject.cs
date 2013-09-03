using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Seedwork
{
    public abstract class VersionedObject
    {
        public byte[] Version { get; set; }
    }
}
