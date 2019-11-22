using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using NSwag;
using NSwag.Generation.Processors.Security;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using projecten3_1920_backend_klim03.Data;
using projecten3_1920_backend_klim03.Data.Repos;
using projecten3_1920_backend_klim03.Domain.Models.Domain;
using projecten3_1920_backend_klim03.Domain.Models.Interfaces;
using projecten3_1920_backend_klim03.helpers;

namespace projecten3_1920_backend_klim03
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IHostingEnvironment Env { get; set; }

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

       


        // test
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy("AllCors", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.KnownProxies.Add(IPAddress.Parse("178.62.218.48"));
            });

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options =>
                {
                    // Add option to ignore looping in JSON response (usefull for N:N relations)
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });

            //services.AddDbContext<ApplicationDbContext>(options =>options.UseSqlServer(Configuration.GetConnectionString("KlimaatMobielContext")));
            //

            if (Env.IsDevelopment())
            {
                string connectionString = $"Server=127.0.0.1;Database=db_klim_local;User=root;Password=root";
                services.AddDbContextPool<ApplicationDbContext>(options => options.UseMySql(connectionString, mySqlOptions =>
                {
                    mySqlOptions.ServerVersion(new Version(8, 0, 17), ServerType.MySql).DisableBackslashEscaping();
                }
                ));
            }
            else
            {
                string connectionString = $"Server=178.62.218.48;Database=db_dev_klim_v2;User=dbklimuser;Password=pwklimuser";
                services.AddDbContextPool<ApplicationDbContext>(options => options.UseMySql(connectionString, mySqlOptions =>
                {
                    mySqlOptions.ServerVersion(new Version(8, 0, 17), ServerType.MySql).DisableBackslashEscaping();
                }
                ));
            }


         

            // Swagger configuration
            // Swagger authentication is included and configured, add [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
            // to the controller class / method to force authentication
            services.AddOpenApiDocument(c =>
            {
                // TODO: Authentication key in project secrets
                c.DocumentName = "apidocs";
                c.Title = "Klimaatmobiel api";
                c.Version = "v2";
                c.Description = "api documentation";
                c.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Description = "Type into the textbox: Bearer {your JWT token}"
                });

                //c.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT Token"));
                c.OperationProcessors.Add(new OperationSecurityScopeProcessor("JWT"));

            });

            services.AddIdentity<AppUser, ApplicationRole>(cfg => cfg.User.RequireUniqueEmail = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddAuthentication(x =>
            {
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
             .AddJwtBearer(x =>
             {
                 x.RequireHttpsMetadata = false;
                 x.SaveToken = true;
                 x.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"])),
                     ValidateIssuer = false,
                     ValidateAudience = false,
                     RequireExpirationTime = false
                 };
             });

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 4;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.RequireUniqueEmail = true;
            });


            services.AddScoped<IOrderRepo, OrderRepo>();
            services.AddScoped<IOrderItemRepo, OrderItemRepo>();
            services.AddScoped<IAppUserRepo, AppUserRepo>();
            services.AddScoped<IGroupRepo, GroupRepo>();
            services.AddScoped<IClassRoomRepo, ClassRoomRepo>();
            services.AddScoped<IProductRepo, ProductRepo>();
            services.AddScoped<IProjectRepo, ProjectRepo>();
            services.AddScoped<ISchoolRepo, SchoolRepo>();
            services.AddScoped<IPupilRepo, PupilRepo>();
            services.AddScoped<IApplicationDomainRepo, ApplicationDomainRepo>();

            services.AddScoped<IProjectTemplateRepo, ProjectTemplateRepo>();
            services.AddScoped<IProductTemplateRepo, ProductTemplateRepo>();


            services.AddScoped<DataInit>();



            // pdf generating
            services.AddScoped<PdfGenerator>();

            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
            //services.AddScoped<TicketPdfGenerator>();

            CustomAssemblyLoadContext context = new CustomAssemblyLoadContext();
            string path = null;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                path = Path.Combine(Directory.GetCurrentDirectory(), "libwkhtmltox.dylib");
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                path = Path.Combine(Directory.GetCurrentDirectory(), "libwkhtmltox.dll");
            }
            else
            {
                path = Path.Combine(Directory.GetCurrentDirectory(), "libwkhtmltox.so");
            }


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, DataInit dataInit)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            app.UseHttpsRedirection();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseAuthentication();

            app.UseCors("AllCors");
            app.UseMvc();

            app.UseSwaggerUi3();
            app.UseOpenApi();

            dataInit.InitializeData().Wait();
        }
    }
}
