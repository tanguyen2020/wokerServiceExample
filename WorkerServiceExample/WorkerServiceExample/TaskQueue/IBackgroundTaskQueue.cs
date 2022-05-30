using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerServiceExample.TaskQueue
{
    public interface IBackgroundTaskQueue: IEnumerable<Action<CancellationToken>>
    {
        void QueueBackgroundWorkItem(Action<CancellationToken> workItem);
        Task<Action<CancellationToken>> DequeueAsync(CancellationToken cancellationToken);
    }
}
