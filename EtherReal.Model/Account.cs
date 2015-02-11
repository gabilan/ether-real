using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherReal.Model
{
    public class Account : EtherRealEntity
    {
        public string Name { get; set; }
        public DbGeography Location { get; set; }
        public AccountStatus Status { get; set; }
        public DateTime? StatusExpirationDate { get; set; }
    }
}
