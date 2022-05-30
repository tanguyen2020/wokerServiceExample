using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerServiceExample.TaskQueue;

namespace WorkerServiceExample
{
    public class WokerServiceTest : HostedService
    {
        private ILoggerFactory _logger;
        protected override int RepeatResult => 5000;
        private IBackgroundTaskQueue _taskQueue;

        public WokerServiceTest(IBackgroundTaskQueue taskQueue, ILoggerFactory logger)
        {
            _taskQueue = taskQueue;
            _logger = logger;
        }
        protected override void OnExecute(object state)
        {
            _taskQueue.QueueBackgroundWorkItem(token => new TestData(_logger.CreateLogger<TestData>()).GetData(token));
        }
    }

    public class TestData
    {
        private ILogger<TestData> _logger;
        public TestData(ILogger<TestData> logger)
        {
            _logger = logger;
        }
        public void GetData(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Worker running at testData: {time}", DateTimeOffset.Now);
        }
    }
}
