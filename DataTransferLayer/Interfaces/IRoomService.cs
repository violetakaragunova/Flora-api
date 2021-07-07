using PlantTrackerAPI.DataTransferLayer.DTO;
using System.Linq;

namespace PlantTrackerAPI.DataTransferLayer.Interfaces
{
    public interface IRoomService
    {
        public IQueryable<RoomDTO> GetRooms();
    }
}
