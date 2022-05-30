using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerServiceExample.TaskQueue
{
    public class BackgroundTaskQueue : IBackgroundTaskQueue
    {
        private ConcurrentQueue<Action<CancellationToken>> _workItems = new ConcurrentQueue<Action<CancellationToken>>();
        private SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(0);
        public async Task<Action<CancellationToken>> DequeueAsync(CancellationToken cancellationToken)
        {
            await _semaphoreSlim.WaitAsync(cancellationToken);
            _workItems.TryDequeue(out var workItem);
            return workItem;
        }

        public IEnumerator<Action<CancellationToken>> GetEnumerator()
        {
            return _workItems.GetEnumerator();
        }

        public void QueueBackgroundWorkItem(Action<CancellationToken> workItem)
        {
            if(workItem == null)
            {
                throw new ArgumentNullException(nameof(workItem));
            }
            _workItems.Enqueue(workItem);
            _semaphoreSlim.Release();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _workItems.GetEnumerator();
        }
    }
}
