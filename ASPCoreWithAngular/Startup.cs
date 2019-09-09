using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ASPCoreWithAngular.Models;
using System.Text;
using System.Threading.Tasks;
using AspCoreData;
using AspCoreData.Contract;
using AspCoreData.UserAuthentication;
using AspCoreData.Query.UserList;
using AspCoreData.Command.UserAddEdit;
using AspCoreData.Command.UserDelete;
using AspCoreDomainModels.UserAddEdit;
using AspCoreDomainModels.Models;
using AspCoreDomainModels.Parameters;
using AspCoreData.Query.UserById;
using AspCoreDomainModels.Models.UserList;
using AspCoreDomainModels.Models.EmployeerProfileAddEdit;
using AspCoreData.Query.EmpProfile;
using AspCoreData.Command.EmployerProfileAddEdit;

namespace ASPCoreWithAngular
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
            // Add Cors
            services.AddCors(o => o.AddPolicy("Cors", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowCredentials();
            }));

            services.Configure<TokenManagement>(Configuration.GetSection("TokenManagement"));

            services.AddMvc();
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory("Cors"));
            });

            var token = Configuration.GetSection("TokenManagement").Get<TokenManagement>();
            var secret = Encoding.ASCII.GetBytes(token.Secret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.RefreshOnIssuerKeyNotFound = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secret),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = System.TimeSpan.Zero
                };
                x.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }
                        return Task.CompletedTask;
                    }
                };
            });

            services.AddScoped<IAuthenticateService, TokenAuthenticationService>();
            services.AddScoped<IUnitOfWork, UnitOfWork<SchoolDataContext>>();
            services.AddScoped<IUserAuthentication, UserAuthentication>();
            services.AddScoped(typeof(ICommand<UserAddEditModel>), typeof(UserAddEditCommand));
            services.AddScoped(typeof(ICommand<int>), typeof(UserDeleteCommand));
            services.AddScoped<IUserList, UserListQuery>();
            services.AddScoped(typeof(IQueryWithParameters<UserAddEditModel, UserByIdParameter>), typeof(UserByIdQuery));
            services.AddScoped(typeof(IQueryWithParameters<QueryResult<UserListPaginationModel>, UserListParameter>), typeof(UserListPaginationQuery));
            services.AddScoped(typeof(ICommand<EmployeerProfileModel>), typeof(EmployerProfileCommand));
            services.AddScoped(typeof(IQueryWithParameters<QueryResult<EmployeerProfileModel>, EmployerProfileParameters>), typeof(EmployeerProfileQuery));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Login/Index");
            }

            app.UseStaticFiles();

            app.UseCors("Cors");
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Login}/{action=Index}/{id?}");


                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Login", action = "Index" });
            });


        }
    }
}
