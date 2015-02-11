using EtherReal.Model;
using Pop3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmtpSyncWorkerRole
{
    class Pop3Syncer
    {
        public static void Sync(Pop3Sync s)
        {
            using (var context = new EtherRealDbContext())
            {
                using (var client = new Pop3Client())
                {
                    client.Connect(s.Host, s.UserName, s.Password, s.Port, s.UseSsl);
                    if (!client.IsConnected)
                        return;

                    foreach (var x in client.List())
                    {
                        Pop3SyncMessage msg = new Pop3SyncMessage()
                        {
                            Sync = s,
                            From = x.From,
                            Date = x.Date,
                            Subject = x.Subject,
                            Message = x.Body
                        };

                        context.Pop3SyncMessages.Add(msg);
                    }

                    context.SaveChanges();
                }
            }
        }
    }
}
