using Autofac;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using System;
using User.Infrastructure;
using User.Web.Infrastructure;
using User.Web.Infrastructure.Extensions;

namespace User.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        /// <summary>
        /// 服务
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            //加载http上下文
            services.AddHttpContextAccessor();
            IdentityModelEventSource.ShowPII = true;//显示错误的详细信息并查看问题
            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
            services.AddApplicationDbContext(Configuration);//DbContext上下文
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddAutoMapper(typeof(Startup));//autoMapper
            services.AddMediatR(typeof(Startup));//CQRS
            services.AddHealthChecks();//健康检查
            services.AddEventBus(Configuration);//集成事件
            services.AddAuthService(Configuration);//认证服务
            services.AddApiVersion();//api版本
            services.AddController();//api控制器s
        }
        /// <summary>
        /// 中间件管道
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseHealthChecks("/health");
            app.RegisterToConsul(Configuration, lifetime);
            app.UseApiVersioning();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            DbContextSeed.SeedAsync(app).Wait();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new DependencyRegistrar());
        }
    }
}
