using API.Extensions;
using API.Middleware;
using API.SignalR;
using Microsoft.OpenApi.Models;

namespace API
{
    public class Startup
    {

        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
  
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationServices(_config);
            services.AddControllers();
            services.AddCors();
            services.AddIdentityServices(_config);
            services.AddSignalR();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPIv5", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {   

            app.UseMiddleware<ExceptionMiddleware>();

            // Redirects if we get into a http endpoint
            app.UseHttpsRedirection();
            
            app.UseRouting();

            app.UseCors(policy => policy.AllowAnyHeader()
                .AllowAnyMethod()
                // Allow credentials since we need to send up our access token
                .AllowCredentials()
                .WithOrigins("https://localhost:4200"));

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                //Setting up endpoint for our presencehub
                endpoints.MapHub<PresenceHub>("hubs/presence");
                //Setting up endpoint for our messagehub
                endpoints.MapHub<MessageHub>("hubs/message");
            });
        }
    }
}
