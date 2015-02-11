using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherReal.Model
{
    public class EtherRealDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Terminal> Terminals { get; set; }
        public DbSet<SmtpSync> SmtpSyncs { get; set; }
        public DbSet<SmtpSyncMessage> SmtpSyncMessages { get; set; }
        public DbSet<Pop3Sync> Pop3Syncs { get; set; }
        public DbSet<Pop3SyncMessage> Pop3SyncMessages { get; set; }
    }
}
