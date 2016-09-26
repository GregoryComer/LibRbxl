using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class TaskScheduler : Instance
    {
        public override string ClassName => "TaskScheduler";

        public bool AreArbitersThrottled { get; set; }
        public ConcurrencyModel Concurrency { get; set; }
        [RobloxIgnore]
        public double NumRunningJobs { get; set; }
        [RobloxIgnore]
        public double NumSleepingJobs { get; set; }
        [RobloxIgnore]
        public double NumWaitingJobs { get; set; }
        public PriorityMethod PriorityMethod { get; set; }
        public double SchedulerDutyCycle { get; set; }
        public double SchedulerRate { get; set; }
        public SleepAdjustMethod SleepAdjustMethod { get; set; }
        public double ThreadAffinity { get; set; }
        public ThreadPoolConfig ThreadPoolConfig { get; set; }
        public int ThreadPoolSize { get; set; }
        public double ThrottledJobSleepTime { get; set; }
    }
}
