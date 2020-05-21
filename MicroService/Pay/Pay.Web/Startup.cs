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
        /// ����
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            //����http������
            services.AddHttpContextAccessor();
            IdentityModelEventSource.ShowPII = true;//��ʾ�������ϸ��Ϣ���鿴����
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
        /// �м���ܵ�
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
        /// ����ע��
        /// </summary>
        private void OnStart()
        {
            var serviceConfig = Configuration.GetSection("ApplicationConfiguration").GetSection("SerivceAddress");
            var client = new ConsulClient(ConsulConfig);
            ///�������
            var httpCheck = new AgentServiceCheck()
            {
                DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(10),//�������1����֮��ȡ������
                Interval = TimeSpan.FromSeconds(10),///�������
                HTTP = serviceConfig.GetSection("HttpType").Value + serviceConfig.GetSection("Address").Value + ":" + Convert.ToInt32(serviceConfig.GetSection("Port").Value) + "/health"
            };
            ///����ע��
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
