using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using N_Tier.Core;
using N_Tier.DataAccess;
using N_Tier.DataAccess.Repositories;
using N_Tier.Core.Services;
using N_Tier.Application.Services;
using FluentValidation.AspNetCore;
using N_Tier.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;


namespace N_Tier.API
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<SpecializationRepository>();
            services.AddScoped<DoctorRepository>();
            services.AddScoped<PatientRepository>();
            services.AddScoped<OrderRepository>();

            services.AddScoped<ISpecializationService, SpecializationService>();
            services.AddScoped< DoctorService>();
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IOrderService, OrderService>();

            // Add Identity
            services.AddDbContext<DatabaseContext>(options =>
         options.UseInMemoryDatabase(_configuration.GetConnectionString("DefaultConnection")));

            // Add authentication and authorization
            // Configure other services as needed
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseHttpsRedirection();

            app.UseCors(corsPolicyBuilder =>
                corsPolicyBuilder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
            );

            app.UseSwagger();

            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Library Management System"); });

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            // Add missing middleware here if needed

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
