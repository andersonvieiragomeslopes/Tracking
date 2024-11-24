#if ANDROID
using AndroidX.Work;
using MobileTracking.Platforms.Android;

#elif IOS
using BackgroundTasks;
using Shared.Mobile.Services;

#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileTracking.Services
{
    public interface IInitalizeBackgroundService
    {
        void StartSyncOrders();
    }
    public class InitalizeBackgroundService : IInitalizeBackgroundService
    {
        public InitalizeBackgroundService()
        {
            
        }
        public void StartSyncOrders()
        {
#if ANDROID
            Constraints constraints = new Constraints.Builder().SetRequiredNetworkType(NetworkType.Connected)
                .SetRequiresCharging(false)
                .SetRequiresBatteryNotLow(true)
                .SetRequiresDeviceIdle(false)
                .SetRequiresStorageNotLow(false)
                .SetRequiredNetworkType(NetworkType.Connected)
                .Build();
            var workRequest = new PeriodicWorkRequest.Builder(
               typeof(BackgroundSyncWorker),
               TimeSpan.FromHours(5))
               .AddTag(BackgroundSyncWorker.TAG)
               .SetConstraints(constraints)
               .Build();
         
            WorkManager.GetInstance(Platform.AppContext).EnqueueUniquePeriodicWork(
                BackgroundSyncWorker.TAG,
                ExistingPeriodicWorkPolicy.Keep,
                workRequest);
#elif IOS
           const string TaskId = "com.companyname.mobiletracking";
            async Task StartTask(BGAppRefreshTask task)
            {
                var response = ServiceLocator.Instance.Resolve<IApiRequestService>();
                var orders = await response.MyOrdersAsync();
                task.SetTaskCompleted(true);
            }
            BGTaskScheduler.Shared.Register(TaskId, null, async (task) =>
        {
           await StartTask(task as BGAppRefreshTask);
        });
        
#endif
    }

    }
}
