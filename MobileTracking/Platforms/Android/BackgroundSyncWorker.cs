using Android.App;
using Android.Content;
using Android.Media;
using AndroidX.Concurrent.Futures;
using AndroidX.Core.App;
using AndroidX.Work;
using Google.Common.Util.Concurrent;
using Shared.Mobile.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileTracking.Platforms.Android
{
    public class BackgroundSyncWorker : ListenableWorker, CallbackToFutureAdapter.IResolver
    {
        public const string TAG = "BackgroundSyncWorker";
        public Context context;
        public BackgroundSyncWorker(Context context, WorkerParameters workerParams) :
            base(context, workerParams)
        {
            this.context = context;
        }
        public override IListenableFuture SetProgressAsync(Data data)
        {
            return base.SetProgressAsync(data);
        }
        public override IListenableFuture StartWork()
        {
            return CallbackToFutureAdapter.GetFuture(this);
        }

        public Java.Lang.Object AttachCompleter(CallbackToFutureAdapter.Completer p0)
        {

            ShowNotification("Atenção!", "Dados sendo sincronizados em segundo plano.");
            Task.Run(async () =>
            {
                await JobRun();
                return p0.Set(Result.InvokeSuccess());
            });

            return TAG;
        }
        private async Task JobRun()
        {
            try
            {
                var response = ServiceLocator.Instance.Resolve<IApiRequestService>();
                var orders = await response.MyOrdersAsync(true);
            }
            catch (Exception ex)
            {

            }        

        }
        private void ShowNotification(string title, string message)
        {
            var notificationManager = (NotificationManager)ApplicationContext.GetSystemService(Context.NotificationService);
            var notification = new Notification.Builder(ApplicationContext)
                .SetContentTitle(title)
                .SetContentText(message)
                .SetSmallIcon(Resource.Drawable.ic_call_answer)
                .Build();

            notificationManager.Notify(1, notification);
        }
    }
}
