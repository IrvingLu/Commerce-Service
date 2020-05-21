using Consul;
using Identity.Core.Domain;
using Identity.Data;
using Identity.Service.Identity;
using Identity.Web.Controller;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Identity.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private readonly string serviceId = Guid.NewGuid().ToString();
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(
                 Configuration.GetConnectionString("DefaultConnection")));
            //身份验证配置
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders()
                    .AddClaimsPrincipalFactory<ClaimsPrincipalFactory>();
            //认证服务器配置
            services.AddIdentityServer()
                    .AddDeveloperSigningCredential()
                    .AddInMemoryIdentityResources(IdentityConfig.GetIdentityResources())
                    .AddInMemoryApiResources(IdentityConfig.GetApiResources())
                    .AddInMemoryClients(IdentityConfig.GetClients())
                    .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>()
                    .AddProfileService<ProfileService>();
            services.AddHealthChecks();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime lifetime)
        {
            app.UseHealthChecks("/health");
            lifetime.ApplicationStarted.Register(OnStart);
            lifetime.ApplicationStopped.Register(OnStopped);
            app.UseIdentityServer();
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
