using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SilkierQuartz.Example.Jobs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Quartz;
using SilkierQuartz.Middlewares;

namespace SilkierQuartz.Example
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddSilkierQuartz();
            services.AddHttpContextAccessor();
            services.AddOptions();
            services.Configure<AppSettings>(Configuration);
            services.Configure<InjectProperty>(options => { options.WriteText = "This is inject string"; });
            services.AddQuartzJob<HelloJob>()
                    .AddQuartzJob<InjectSampleJob>()
                    .AddQuartzJob<HelloJobSingle>()
                    .AddQuartzJob<InjectSampleJobSingle>();

            // Use cookie authentication.
            services.AddAuthentication(GlobalConfig.AuthConfig.AuthScheme).AddCookie(GlobalConfig.AuthConfig.AuthScheme,
                config =>
                {
                    config.Cookie.Name = GlobalConfig.AuthConfig.AuthCookieName;
                    config.LoginPath = "/Error";
                    config.AccessDeniedPath = "/Index";
                    config.ExpireTimeSpan = TimeSpan.FromSeconds(GlobalConfig.AuthConfig.SessionIdleValid);
                    config.SlidingExpiration = true;
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(GlobalConfig.AuthConfig.AuthScheme, p =>
                {
                    p.RequireAuthenticatedUser();
                    p.Build();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseMiddleware<SilkierQuartzAuthenticationMiddleware>();
            app.UseAuthorization();
            app.UseSilkierQuartz(
                new SilkierQuartzOptions()
                {
                    VirtualPathRoot = "/SilkierQuartz",
                    UseLocalTime = true,
                    DefaultDateFormat = "yyyy-MM-dd",
                    DefaultTimeFormat = "HH:mm:ss",
                    CronExpressionOptions = new CronExpressionDescriptor.Options()
                                            {
                                                DayOfWeekStartIndexZero = false //Quartz uses 1-7 as the range
                                            },
                    AccountName = "admin",
                    AccountPassword = "password",
                    IsAuthenticationPersist = false
                }
                );
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();

                //endpoints.MapControllerRoute(
                //    name: "defaultMVC",
                //    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            //How to compatible old code to SilkierQuartz
            //���ɵ�ԭ���Ĺ滮Job�Ĵ��������ֲ���ݵ�ʾ��
            // app.SchedulerJobs();


            #region  ��ʹ�� SilkierQuartzAttribe ���ԵĽ���ע���ʹ�õ�IJob������ͨ��UseQuartzJob��IJob������  ConfigureServices����AddQuartzJob

            app.UseQuartzJob<HelloJobSingle>(TriggerBuilder.Create().WithSimpleSchedule(x => x.WithIntervalInSeconds(1).RepeatForever()))
            .UseQuartzJob<InjectSampleJobSingle>(() =>
            {
                return TriggerBuilder.Create()
                   .WithSimpleSchedule(x => x.WithIntervalInSeconds(1).RepeatForever());
            });

            app.UseQuartzJob<HelloJob>(new List<TriggerBuilder>
                {
                    TriggerBuilder.Create()
                    .WithSimpleSchedule(x => x.WithIntervalInSeconds(1).RepeatForever()),
                    TriggerBuilder.Create()
                    .WithSimpleSchedule(x => x.WithIntervalInSeconds(2).RepeatForever()),
                     //Add a sample that uses 1-7 for dow
                    TriggerBuilder.Create()
                                  .WithCronSchedule("0 0 2 ? * 7 *"),
                });

            app.UseQuartzJob<InjectSampleJob>(() =>
            {
                var result = new List<TriggerBuilder>();
                result.Add(TriggerBuilder.Create()
                    .WithSimpleSchedule(x => x.WithIntervalInSeconds(10).RepeatForever()));
                return result;
            });
            #endregion
        }
    }
}
