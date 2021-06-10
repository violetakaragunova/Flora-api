using PlantTrackerAPI.DomainModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PlantTrackerAPI.DataTransferLayer.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(User user);
    }
}
