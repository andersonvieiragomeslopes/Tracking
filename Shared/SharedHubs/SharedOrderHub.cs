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
                        return "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiOWQyMWZhYTItMjg3MC00NTNiLWIzM2UtYWRkZDcwNGRhZGVhIiwiZXhwIjoxNzM1MDc0Mjg3fQ.pFnh9YwiNjSm80fgPUpRXcuSjjSk4EysEu9FfirTEcQ";
                    };
                })
                    .WithAutomaticReconnect().Build();
            _connection.On<Guid>("NewOrder", NewOrder);
            _connection.Closed += async (error) =>
            {
               // await Task.Delay(new Random().Next(0, 5) * 1000);
                
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
             await _connection.StartAsync();
        }

        public Task StopAsync()
        {
            return _connection.StopAsync();
        }
    }
}
