using Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Mobile.Repositories
{
    public interface IOrdersRepository
    {
        Task<IEnumerable<OrderRepository>> GetAllAsync();
        Task SaveAllAsync(IEnumerable<OrderResponse> orderResponses);
        Task SaveAsync(OrderResponse orderResponse);
    }
    public class OrdersRepository : ApplicationDbContext<BaseRepository>, IOrdersRepository
    {

        public async Task SaveAllAsync(IEnumerable<OrderResponse> orderResponses)
        {
            var list = new List<OrderRepository>();
            foreach (var item in orderResponses)
            {
                var repository = new OrderRepository
                {
                    CreatedAt = item.CreatedAt,
                    UpdatedAt = item.UpdatedAt,
                    Id = item.Id,
                    Description = item.Description,
                    Image = item.Image,
                    Latitude = item.Latitude,
                    Longitude = item.Longitude,
                    Title = item.Title,
                    UserId = item.UserId,
                };
                list.Add(repository);
            }
            await InsertOrReplaceAllAsync(list);
        }

        public async Task SaveAsync(OrderResponse orderResponse)
        {
            var repository = new OrderRepository
            {
                CreatedAt = orderResponse.CreatedAt,
                UpdatedAt = orderResponse.UpdatedAt,
                Id = orderResponse.Id,
                Description = orderResponse.Description,
                Image = orderResponse.Image,
                Latitude = orderResponse.Latitude,
                Longitude = orderResponse.Longitude,
                Title = orderResponse.Title,
                UserId = orderResponse.UserId,
            };

            await InsertOrReplaceAsync(repository);
        }
        public async Task<IEnumerable<OrderRepository>> GetAllAsync()
        {
            return await GetAllAsync();
        }
    }
}
