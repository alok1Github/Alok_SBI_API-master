using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ProjectHandler.API.BusinessLayer;
using ProjectHandler.API.DatabaseLayer;

namespace ProjectHandler.API
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
            services.AddCors();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddTransient<ITaskHandler, API.BusinessLayer.TaskHandler>();

            services.AddTransient<ITaskHandlerRepository, TaskHandlerRepository>();

            services.AddTransient<IProjectHandler, API.BusinessLayer.ProjectHandler>();

            services.AddTransient<IProjectHandlerRepository, ProjectHandlerRepository>();

            services.AddTransient<IUserHandler, API.BusinessLayer.UserHandler>();

            services.AddTransient<IUserHandlerRepository, UserHandlerRepository>();

            services.AddEntityFrameworkSqlServer()
                .AddDbContext<TaskHandlerDbContext>(
                    option => option.UseSqlServer(Configuration.GetSection("Database").GetSection("Connection").Value));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        [ExcludeFromCodeCoverage]
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
