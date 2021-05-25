using DataTransferLayer.DTO;
using System.Threading.Tasks;

namespace DataTransferLayer.Interfaces
{
    public interface IUserService
    {
        public Task<UserDTO> Get(int Id);
    }
}
