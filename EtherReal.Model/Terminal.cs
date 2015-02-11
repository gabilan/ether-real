using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherReal.Model
{
    public class Terminal : EtherRealEntity
    {
        public Account Owner { get; set; }
        public string Name { get; set; }
        public DbGeography Location { get; set; }
        public TerminalFeature Features { get; set; }
        public bool IsPrimary { get; set; }
    }
}
