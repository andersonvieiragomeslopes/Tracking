using Shared.Mobile.Repositories;
using Shared.Responses;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Mobile
{
    //https://learn.microsoft.com/en-us/dotnet/maui/data-cloud/database-sqlite?view=net-maui-9.0
    public interface IApplicationDbContext
    {

    }
    public class ApplicationDbContext<T>
    {
        public ApplicationDbContext()
        {
        }

        private void ApplicationDbContext_TableChanged(object? sender, NotifyTableChangedEventArgs e)
        {
            if (e?.Table?.TableName == "WeatherData.Forecasts.TABLE_NAME")
            {
            }

        }

        public bool DataBaseInitialized
        {
            get
            {
                if (ApplicationDbConnection._connection == null)
                {
                    ApplicationDbConnection.InitializeAsync().Wait();
                }
                return ApplicationDbConnection._connection != null;
            }
        }

        public async Task<bool> InsertOrReplaceAsync<T>(T data) where T : class, new()
        {
            try
            {
                await ApplicationDbConnection._connection.InsertOrReplaceAsync(data);

            }
            catch (Exception ex)
            {

            }
            return true;
        }
        public async Task<IEnumerable<T>> GetAllAsync<T>() where T : class, new()
        {
            if (!DataBaseInitialized)
                await ApplicationDbConnection.InitializeAsync();
            if (ApplicationDbConnection._connection == null) return null;
            return await ApplicationDbConnection._connection.Table<T>().ToListAsync();
        }
        public async Task InsertOrReplaceAllAsync<T>(IEnumerable<T> data) where T : class, new()
        {
            try
            {
                if (!DataBaseInitialized)
                    await ApplicationDbConnection.InitializeAsync();
                if (data == null) return;
                var list = await ApplicationDbConnection._connection.InsertOrReplaceAsync(data);

            }
            catch (Exception ex)
            {

            }
        }
    }
    public static class ApplicationDbConnection
    {
        public static SQLiteAsyncConnection _connection;
        public static async Task InitializeAsync()
        {
            if (ApplicationDbConnection._connection is not null)
                return;

            ApplicationDbConnection._connection = new SQLiteAsyncConnection(Constants.DatabasePath);
            await ApplicationDbConnection._connection.DropTableAsync<OrderRepository>();
            var result = await ApplicationDbConnection._connection.CreateTableAsync<OrderRepository>();
        }
    }
}
