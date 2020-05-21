using AutoMapper;
using Consul;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using Pay.Infrastructure;
using Pay.Web.Infrastructure.Extensions;
using System;

namespace Pay.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private readonly string serviceId = Guid.NewGuid().ToString();
        /// <summary>
        /// 服务
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            //加载http上下文
            services.AddHttpContextAccessor();
            IdentityModelEventSource.ShowPII = true;//显示错误的详细信息并查看问题
            services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Transient);
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddAutoMapper(typeof(Startup));
            services.AddMediatR(typeof(Startup));
            services.AddAuthService(Configuration);
            services.AddEventBus(Configuration);
            services.AddHealthChecks();
            services.AddApiVersion();
            services.AddController();
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
            lifetime.ApplicationStarted.Register(OnStart);
            lifetime.ApplicationStopped.Register(OnStopped);
            app.UseApiVersioning();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        /// <summary>
        /// 服务注册
        /// </summary>
        private void OnStart()
        {
            var serviceConfig = Configuration.GetSection("ApplicationConfiguration").GetSection("SerivceAddress");
            var client = new ConsulClient(ConsulConfig);
            ///健康检查
            var httpCheck = new AgentServiceCheck()
            {
                DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(10),//服务出错1分钟之后，取消服务
                Interval = TimeSpan.FromSeconds(10),///检查周期
                HTTP = serviceConfig.GetSection("HttpType").Value + serviceConfig.GetSection("Address").Value + ":" + Convert.ToInt32(serviceConfig.GetSection("Port").Value) + "/health"
            };
            ///服务注册
            var agentReg = new AgentServiceRegistration()
            {
                ID = serviceId,
                Check = httpCheck,
                Address = serviceConfig.GetSection("Address").Value,
                Port = Convert.ToInt32(serviceConfig.GetSection("Port").Value),
                Name = serviceConfig.GetSection("Name").Value,
            };
            client.Agent.ServiceRegister(agentReg).ConfigureAwait(false);
        }
        private void OnStopped()
        {
            var client = new ConsulClient(ConsulConfig);
            client.Agent.ServiceDeregister(serviceId).ConfigureAwait(false);
        }

        private void ConsulConfig(ConsulClientConfiguration consul)
        {
            consul.Address = new Uri(Configuration.GetSection("ApplicationConfiguration").GetSection("ConsulAddress").Value);
        }

    }
  
}
