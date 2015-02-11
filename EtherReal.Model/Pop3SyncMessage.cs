using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherReal.Model
{
    public class Pop3SyncMessage : EtherRealEntity
    {
        public Pop3Sync Sync { get; set; }
        public string From { get; set; }
        public string Date { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
