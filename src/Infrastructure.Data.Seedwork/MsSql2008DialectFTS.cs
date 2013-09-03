using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Dialect;
using NHibernate.Dialect.Function;

namespace Infrastructure.Data.Seedwork
{
    public class MsSql2008DialectFTS : MsSql2008Dialect
    {
        public MsSql2008DialectFTS()
        {
            RegisterFunction("freetext", new StandardSQLFunction("freetext", null));
            RegisterFunction("contains", new StandardSQLFunction("contains", null));
        }
    }
}
