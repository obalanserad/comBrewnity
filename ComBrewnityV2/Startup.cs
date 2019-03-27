using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ComBrewnityV2.Services;
using Domain;
using Domain.Entities;
using Domain.Migrations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Text;
using ComBrewnityV2.Helpers;
using ComBrewnityV2.Auth;

namespace ComBrewnityV2
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
            // configure strongly typed settings objects
            string cryptic = Configuration["Secret"].ToString();

            // configure jwt authentication
            SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(cryptic));

            // ===== Add our DbContext ========
            services.AddDbContext<ApplicationDbContext>(
                //lazy loading!
                c=>c.UseLazyLoadingProxies()
                .UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), 
          b => b.MigrationsAssembly("Domain")));

            // ===== Add Identity ========
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddDefaultUI()
                .AddRoles<ApplicationRole>()
                .AddRoleManager<RoleManager<ApplicationRole>>()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            //Cors - remove AllowAll later
            services.AddCors(options =>
                options.AddPolicy("AllowAllOrigins", builder =>
                 {
                     builder.AllowAnyOrigin();
                 }));

            //Dependency injection
            services.AddTransient<PostService, PostService>();
            services.AddSingleton<IJwtFactory, JwtFactory>();

            //Mapper
            services.AddAutoMapper();

            //JWT
            //resource: https://fullstackmark.com/post/13/jwt-authentication-with-aspnet-core-2-web-api-angular-5-net-core-identity-and-facebook-login
            var jwtAppSettingOptions = Configuration.GetSection("JwtIssuerOptions");
            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            });
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

                ValidateAudience = true,
                ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _signingKey,

                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(configureOptions =>
            {
                configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                configureOptions.TokenValidationParameters = tokenValidationParameters;
                configureOptions.SaveToken = true;
            });

            // api user claim policy
            services.AddAuthorization(options =>
            {
                options.AddPolicy("User", policy => policy.RequireClaim(Constants.Strings.JwtClaimIdentifiers.Rol, Constants.Strings.JwtClaims.ApiAccess));
            });

            //InitialData.Seed(_serviceProvider, Configuration);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app, 
            IHostingEnvironment env, 
            ApplicationDbContext context,
            RoleManager<ApplicationRole> roleManager,
            UserManager<ApplicationUser> userManager)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            // Shows UseCors with CorsPolicyBuilder.
            app.UseCors(builder =>
               builder.AllowAnyOrigin());

            app.UseAuthentication();

            app.UseHttpsRedirection();
            app.UseMvc();

            InitialData.Seed(Configuration, context, userManager, roleManager).Wait();
        }
    }
}
