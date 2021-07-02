﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using DataAccessLayer;
using DataTransferLayer.DTO;
using DataTransferLayer.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PlantTrackerAPI.DomainModel;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationContext dbContext;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        public UserService(ApplicationContext _dbContext, IMapper mapper, UserManager<User> userManager)
        {
            dbContext = _dbContext;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<UserDTO> GetUserById(int Id)
        {
            User user = await _userManager.FindByIdAsync(Id.ToString()).ConfigureAwait(false);
            return _mapper.Map<UserDTO>(user);
        }

        public IQueryable<UserDTO> GetUsers()
        {
            var users = dbContext.Users.AsQueryable();
            return users.ProjectTo<UserDTO>(_mapper.ConfigurationProvider).AsNoTracking();
        }
    }
}
