using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RMLXCast.Core.Domain.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMLXCast.Services.Catalog.User
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly UserManager<ApplicationUser> userManager;

        public ApplicationUserService(
            UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<IList<ApplicationUser>> GetPagedUsersAsync(int pageNumber, int pageSize)
        {
            return await userManager.Users
                .Include(x => x.Addresses)
                .Include(x => x.ApplicationUserRoles)
                .Include(x => x.Orders)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetTotalUsersAsync()
        {
            return await userManager.Users.CountAsync();
        }
    }
}
