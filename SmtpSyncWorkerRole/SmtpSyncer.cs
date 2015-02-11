using EtherReal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SmtpSyncWorkerRole
{
    class SmtpSyncer
    {
        public static void Sync(SmtpSync s)
        {
            using (var context = new EtherRealDbContext())
            {
                var messagesToSend = from x in context.SmtpSyncMessages
                                     where x.Sync == s
                                     select x;

                using (var client = new SmtpClient(s.Host, s.Port))
                {
                    client.Credentials = new NetworkCredential(s.UserName, s.Password);
                    client.UseDefaultCredentials = false;
                    client.SendCompleted += (a, b) =>
                    {
                        long messageId = (long)b.UserState;
                        using (var messageRemovalContext = new EtherRealDbContext())
                        {
                            messageRemovalContext.SmtpSyncMessages.Remove(messageRemovalContext.SmtpSyncMessages.Find(messageId));
                            messageRemovalContext.SaveChanges();
                        }
                    };


                    foreach (var x in messagesToSend)
                    {
                        try
                        {
                            MailMessage msg = new MailMessage(s.EmailAddress, x.EmailAddress, x.Subject, x.Message);
                            client.SendAsync(msg, x.Id);
                        }
                        catch (Exception e)
                        {

                        }
                    }
                }
            }
        }
    }
}