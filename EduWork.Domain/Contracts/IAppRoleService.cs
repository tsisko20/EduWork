using EduWork.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduWork.Domain.Contracts
{
    internal interface IAppRoleService
    {
        Task<UserAppRoleDTO> GetUserAppRoleAsync(int userId); 
    }
}
