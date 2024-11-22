using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileTracking
{
    public abstract partial class BaseViewModel : ObservableObject, IQueryAttributable
    {
        public virtual Task InitializeAsync(object navigationData)
        {
            InitializeAsync();
            return Task.FromResult(false);
        }
        protected virtual Task InitializeAsync()
        {
            return Task.FromResult(true);
        }
        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            await InitializeAsync(query.Values.FirstOrDefault()).ConfigureAwait(false);
        }
    }
}
