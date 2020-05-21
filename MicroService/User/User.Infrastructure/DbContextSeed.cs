/// ***********************************************************************
///
/// =================================
/// CLR版本    ：4.0.30319.42000
/// 命名空间    ：User.Infrastructure.EntityFrameworkCore
/// 文件名称    ：DbContextSeed.cs
/// =================================
/// 创 建 者    ：鲁岩奇
/// 创建日期    ：2019/10/28 13:53:49 
/// 功能描述    ：
/// 使用说明    ：
/// =================================
/// 修改者    ：
/// 修改日期    ：
/// 修改内容    ：
/// =================================
///
/// ***********************************************************************

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;
using User.Core.Domain.Authorization;

namespace User.Infrastructure
{

    public class DbContextSeed
    {
        public static async Task SeedAsync(IApplicationBuilder applicationBuilder)
        {
            try
            {
                using var scope = applicationBuilder.ApplicationServices.CreateScope();
                var context = (ApplicationDbContext)scope.ServiceProvider.GetService(typeof(ApplicationDbContext));
                context.Database.Migrate();
                if (!context.Users.Any())
                {
                    var user = new ApplicationUser
                    {
                        UserName = "admin",
                        Email = "sfan_technology163.com",
                        PhoneNumber = "024-31681272",
                    };
                    user.PasswordHash = new PasswordHasher<IdentityUser>(new OptionsWrapper<PasswordHasherOptions>(new PasswordHasherOptions())).HashPassword(user, "123456");
                    user.NormalizedUserName = user.UserName.ToUpper();
                    context.Users.Add(user);
                    await context.SaveChangesAsync();
                };
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
