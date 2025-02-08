using Microsoft.OpenApi.Models;
using ProjectTester.Services.Provider;

namespace ProjectTester.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        /// <summary>
        /// Gets called by the runtime. Add services to the container.
        /// </summary>
        /// <param name="services">Serivces configuration.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ITransactionProvider, TransactionProvider>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyApi", Version = "v1" });
            });
        }

        /// <summary>
        /// Gets called by the runtime. Configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">App configuration.</param>
        /// <param name="env">Env configuration.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}