using System.Linq;
using System.Threading.Tasks;
using HomeApi.Contracts.Models.Rooms;
using HomeApi.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace HomeApi.Data.Repos
{
    /// <summary>
    /// Репозиторий для операций с объектами типа "Room" в базе
    /// </summary>
    public class RoomRepository : IRoomRepository
    {
        private readonly HomeApiContext _context;
        
        public RoomRepository (HomeApiContext context)
        {
            _context = context;
        }
        
        /// <summary>
        ///  Найти комнату по имени
        /// </summary>
        public async Task<Room> GetRoomByName(string name)
        {
            return await _context.Rooms.Where(r => r.Name == name).FirstOrDefaultAsync();
        }
        
        /// <summary>
        ///  Добавить новую комнату
        /// </summary>
        public async Task AddRoom(Room room)
        {
            var entry = _context.Entry(room);
            if (entry.State == EntityState.Detached)
                await _context.Rooms.AddAsync(room);
            
            await _context.SaveChangesAsync();
        }

        public async Task EditRoom(Room room, EditRoomRequest request)
        {
            if (!string.IsNullOrEmpty(request.Name) && request.Name != room.Name)
            {
                room.Name = request.Name;
            }
            if (request.Voltage != room.Voltage)
            {
                room.Voltage = request.Voltage;
            }
            if (request.Area != room.Area)
            {
                room.Area = request.Area;
            }
            if (request.GasConnected != room.GasConnected)
            {
                room.GasConnected = request.GasConnected;
            }

            var entry = _context.Entry(room);
            if (entry.State == EntityState.Detached)
                _context.Rooms.Update(room);

            await _context.SaveChangesAsync();
        }
    }
}