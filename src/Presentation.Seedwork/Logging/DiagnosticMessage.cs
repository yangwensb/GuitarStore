using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Seedwork.Logging
{
    public class DiagnosticMessage
    {
        const string DEFAULT_STRING = "?";
        const int DEFAULT_NUMERIC = -1;

        public DiagnosticMessage()
        {
            MessageFormat = "^session:{0}^ip:{1}^orgId:{2}^personId:{3}";
        }

        public readonly string MessageFormat;
        public object[] Parameters
        {
            get { 
                var list = new List<object>();

                list.Add(Session ?? DEFAULT_STRING);
                list.Add(IP ?? DEFAULT_STRING);
                list.Add(OrgId != 0 ? OrgId : DEFAULT_NUMERIC);
                list.Add(PersonId != 0 ? PersonId : DEFAULT_NUMERIC);

                return list.ToArray();
            }
        }

        public string Session { get; set; }
        public string IP { get; set; }
        public int OrgId { get; set; }
        public int PersonId { get; set; }
    }
}
