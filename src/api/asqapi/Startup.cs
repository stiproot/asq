using asqapi.Extensions;
using asqapi.Models;
using managers.Extensions;
using dbaccess.Extensions;
using processes.Extensions;
using ZoomClient.Extensions;
using MailClient.Extensions;
using Caching.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpOverrides;
using System.Text;

namespace asqapi
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
            services.AddCors(options => 
            { 
                options.AddPolicy("CorsPolicy", builder => builder
                    .WithOrigins
                    (
                        "http://localhost:4200", 
                        "http://localhost:81",
                        "http://asq.properties",
                        "http://www.asq.properties"
                    )
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()); 
            });
            services.AddMemoryCache();
            services.AddControllers();

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders 
                    = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });
            services.Configure<ApiCaching>(this.Configuration.GetSection(nameof(ApiCaching)));
            services.Configure<StaticFileServerSettings>(this.Configuration.GetSection(nameof(StaticFileServerSettings)));

            services.AddAuthenticationService(this.Configuration);
            services.AddProviderServices(this.Configuration);

            var key = Encoding.ASCII.GetBytes(Configuration["AppSettings:Secret"]);
            services.AddAuthentication(a => 
            {
                a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(b => 
            {
                b.RequireHttpsMetadata = false;
                b.SaveToken = true;
                b.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.Configure<FormOptions>(x => {
              x.ValueLengthLimit = int.MaxValue;
              x.MultipartBodyLengthLimit = int.MaxValue; // In case of multipart
            });

            services.AddDbAccessServices(this.Configuration);
            services.AddResourceManagerServices(this.Configuration);
            services.AddProcessesServices(this.Configuration);
            services.AddZoomClientServices(this.Configuration);
            services.AddMailClientServices(this.Configuration);
            services.AddCachingServices(this.Configuration);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseForwardedHeaders();
            }
            else
            {
                app.UseForwardedHeaders();
            }

            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
