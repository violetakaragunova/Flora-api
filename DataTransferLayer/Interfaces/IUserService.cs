using DataTransferLayer.DTO;
using System.Linq;
using System.Threading.Tasks;

namespace DataTransferLayer.Interfaces
{
    public interface IUserService
    {
        public Task<UserDTO> GetUserById(int Id);
        public IQueryable<UserDTO> GetUsers();
    }
}
