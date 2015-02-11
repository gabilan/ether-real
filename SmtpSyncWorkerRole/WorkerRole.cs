using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using EtherReal.Model;

namespace SmtpSyncWorkerRole
{
    public class WorkerRole : RoleEntryPoint
    {
        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private readonly ManualResetEvent runCompleteEvent = new ManualResetEvent(false);

        public override void Run()
        {
            Trace.TraceInformation("SmtpSyncWorkerRole is running");

            try
            {
                List<Task> tasks = new List<Task>();

                tasks.Add(this.RunSmtpAsync(this.cancellationTokenSource.Token));
                tasks.Add(this.RunPop3Async(this.cancellationTokenSource.Token));

                Task.WaitAll(tasks.ToArray());
            }
            finally
            {
                this.runCompleteEvent.Set();
            }
        }

        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections
            ServicePointManager.DefaultConnectionLimit = 12;

            // For information on handling configuration changes
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.

            bool result = base.OnStart();

            Trace.TraceInformation("SmtpSyncWorkerRole has been started");

            return result;
        }

        public override void OnStop()
        {
            Trace.TraceInformation("SmtpSyncWorkerRole is stopping");

            this.cancellationTokenSource.Cancel();
            this.runCompleteEvent.WaitOne();

            base.OnStop();

            Trace.TraceInformation("SmtpSyncWorkerRole has stopped");
        }

        private async Task RunSmtpAsync(CancellationToken cancellationToken)
        {
            // TODO: Replace the following with your own logic.
            while (!cancellationToken.IsCancellationRequested)
            {
                Trace.TraceInformation("Syncing Smtp");

                try
                {
                    using (var context = new EtherRealDbContext())
                    {
                        Parallel.ForEach(context.SmtpSyncs, x => SmtpSyncer.Sync(x));
                    }
                }
                catch (Exception e)
                {
                    Trace.TraceError(e.Message);
                    Trace.TraceError(e.StackTrace);
                }

                await Task.Delay(1000);
            }
        }

        private async Task RunPop3Async(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                Trace.TraceInformation("Syncing POP3");

                try
                {
                    using (var context = new EtherRealDbContext())
                    {
                        Parallel.ForEach(context.Pop3Syncs, x => Pop3Syncer.Sync(x));
                    }
                }
                catch (Exception e)
                {
                    Trace.TraceError(e.Message);
                    Trace.TraceError(e.StackTrace);
                }

                await Task.Delay(1000);
            }
        }
    }
}
