using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.SharedHubs
{
    public interface ISharedOrderHub
    {
        Task StartAsync();
        Task StopAsync();

        event EventHandler<Guid> OnOrderAdd;
    }
    public class SharedOrderHub : ISharedOrderHub
    {
        public event EventHandler<Guid> OnOrderAdd;
        private HubConnection _connection;
        public SharedOrderHub()
        {
            _connection = new HubConnectionBuilder()
                .WithUrl("https://tracking.bicicreteiro.app/orders", (options) =>
                {
                    options.AccessTokenProvider = async () =>
                    {
                        return ApiConstants.JTW_DEBUG;
                    };
                })
                    .WithAutomaticReconnect().Build();
            _connection.On<Guid>("NewOrder", NewOrder);
            _connection.Closed += async (error) =>
            {
                
            };
            _connection.Reconnecting += Connection_Reconnecting;
        }

        private async Task Connection_Reconnecting(Exception? arg)
        {

        }

        public async Task NewOrder(Guid id)
        {
            OnOrderAdd?.Invoke(this, id);
        }
        public async Task StartAsync()
        {
            try
            {
                if(_connection?.State != HubConnectionState.Connected)
                await _connection.StartAsync();
            }
            catch (ThreadAbortException ex)
            {

            }
            catch (ThreadInterruptedException ex)
            {

            }
            catch (OperationCanceledException ex)
            {

            }
        }

        public async Task StopAsync()
        {
            try
            {
                if (_connection?.State == HubConnectionState.Connected)
                    await _connection.StopAsync();
            }
            catch (ThreadAbortException ex)//
            {
                
            }
            catch (ThreadInterruptedException ex)//
            {

            }
            catch (OperationCanceledException ex)
            {

            }
        }
    }
}
