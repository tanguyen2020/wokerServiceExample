using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerServiceExample
{
    public abstract class HostedService : IHostedService, IDisposable
    {
        private Timer? _timer;
        protected abstract int RepeatResult { get; }
        protected abstract void OnExecute(object state);
        public Task StartAsync(CancellationToken cancellationToken)
        {
            #pragma warning disable CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
            _timer = new Timer(ExecuteTask, null, TimeSpan.Zero, TimeSpan.FromMilliseconds(RepeatResult));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        private void ExecuteTask(object state)
        {
            OnExecute(state);
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
