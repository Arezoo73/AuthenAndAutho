using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace AuthenAndAutho
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddControllers().AddNewtonsoftJson(Options => Options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ToDo API",
                });
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminAndPowerUser", policy => policy.RequireRole("Administrator", "Poweruser"));

                options.AddPolicy("EmployeeWithMoreThan20Years", policy => policy.Requirements.Add(new EmployeeWithMoreYearsRequirment(20)));

                options.AddPolicy("Claim.DoB", policy => policy.RequireClaim(ClaimTypes.DateOfBirth));
               
            });
            services.AddAuthentication("Basic").AddScheme<BasicAuthenticationsOptions, CustomAuthenticationHandler>("Basic", null);
            services.AddScoped<IAuthorizationHandler, CustomRequireClaimHandler>();
            services.AddScoped<IAuthorizationHandler, EmployeeWithMoreYearsHandler>();
            services.AddScoped<IEmployeeNumberOfYears, EmployeeNumberOfYears>();
            services.AddScoped<ICustomAuthenticationManager, CustomAuthenticationManager>();
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

            });
        }
    }
}
