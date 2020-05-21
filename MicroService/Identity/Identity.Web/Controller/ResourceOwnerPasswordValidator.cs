using Identity.Core.Domain;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Identity.Web.Controller
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        public ResourceOwnerPasswordValidator(UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _signInManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
        }
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            try
            {
                ApplicationUser user = null;
                if (context.UserName == "anonymous@wonbery.com")
                {
                    user = await userManager.FindByNameAsync(context.UserName);
                    if (user == null)
                    {
                        user = new ApplicationUser
                        {
                            Id = Guid.NewGuid().ToString(),
                            UserName = context.UserName,
                            Email = context.UserName,
                            Avatar = "avatar.jpg"
                        };
                        await userManager.CreateAsync(user, context.Password);
                        ///匿名认证成功，返回token
                        context.Result = new GrantValidationResult(user.Id.ToString(), user.UserName, GetUserClaim(user));
                    }
                }
                else
                {
                    user = await userManager.FindByNameAsync(context.UserName);
                    ///判断用户是否存在
                    if (user != null)
                    {

                        var result = await signInManager.PasswordSignInAsync(context.UserName, context.Password, false, lockoutOnFailure: false);
                        ///判断验证是否成功
                        if (result.Succeeded)
                        {
                            bool islocked = await userManager.GetLockoutEnabledAsync(user);
                            ///验证用户是否锁定
                            if (islocked)
                            {
                                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "用户已锁定，请联系管理员解锁");
                                //Console.WriteLine("用户已经被锁定");
                                return;
                            }
                            else
                            {
                                ///重新计算失败次数
                                await userManager.ResetAccessFailedCountAsync(user);
                                ///认证成功，返回token
                                context.Result = new GrantValidationResult(user.Id.ToString(), user.UserName, GetUserClaim(user));
                            }
                        }
                        else
                        {
                            ///记录失败次数
                            await userManager.AccessFailedAsync(user);
                            int accessFailedCount = await userManager.GetAccessFailedCountAsync(user);
                            ///输入5次错误密码锁定账户
                            if (accessFailedCount == 5)
                            {
                                await userManager.AccessFailedAsync(user);
                                await userManager.SetLockoutEnabledAsync(user, true);
                            }
                            context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "用户密码错误");
                            return;
                        }
                    }
                    else
                    {
                        context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "此用户名不存在");
                        return;
                    }

                }
            }
            catch (Exception)
            {
                //验证失败
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "invalid custom credential");
                throw;
            }
        }
        public Claim[] GetUserClaim(ApplicationUser userInfo)
        {
            var claims = new Claim[] {
                new Claim("Id", userInfo.Id),
                new Claim("Name", userInfo.UserName)
            };
            return claims;
        }


    }
}
