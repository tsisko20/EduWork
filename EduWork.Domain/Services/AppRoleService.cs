using AutoMapper;
using EduWork.Common.DTO;
using EduWork.Data;
using EduWork.Data.Entities;
using EduWork.Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduWork.Domain.Services
{
    public class AppRoleService(AppDbContext context, IMapper mapper) : IAppRoleService
    {
       
        public async Task<UserAppRoleDTO> GetUserAppRoleAsync(int userId)
        {
            var user = await context.Users
                .Include(u => u.AppRole)
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null) {
                throw new KeyNotFoundException($"User with ID {userId} not found.");
            }

            var userAppRoleDTO = mapper.Map<UserAppRoleDTO>(user);

            return userAppRoleDTO;
        }
    }
}
