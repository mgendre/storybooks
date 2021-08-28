using System;
using System.Text.Json.Serialization;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using StoryBooks.Api.Infra;
using StoryBooks.Api.Infra.Jwt;
using StoryBooks.DocumentLib.Infra;
using StoryBooks.Shared.Cosmos;
using StoryBooks.Shared.Infra;

namespace StoryBooks.Api
{
    public class Startup
    {
        
        private readonly IConfiguration _configuration;
        
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(opts =>
            {
                var enumConverter = new JsonStringEnumConverter();
                opts.JsonSerializerOptions.Converters.Add(enumConverter);
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "StoryBooks.Api", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "Please enter into field the word 'Bearer' following by space and JWT",
                    Type = SecuritySchemeType.ApiKey,
                    In = ParameterLocation.Header,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
            services.AddMediatR(typeof(Startup));
            services.AddHttpContextAccessor();

            try
            {
                var cosmosDbConfig = _configuration.GetSection("CosmosDb").Get<CosmosDbSettings>();
                services.AddCosmosDb(cosmosDbConfig);
                
                var mediaLibOptions = _configuration.GetSection("MediaLib");
                services.AddMediaLib(mediaLibOptions, cosmosDbConfig);
                
                services.AddSharedModule(cosmosDbConfig);
            }
            catch (Exception e)
            {
                // This error is thrown by nswag for an obscure reason
                Console.Error.WriteLine("Error while initializing Cosmos: " + e);
            }

            var googleClientId = _configuration.GetSection("Authentication:Google:ClientId").Get<string>();
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                })
                .AddJwtBearer(o =>
                {
                    o.SecurityTokenValidators.Clear();
                    o.SecurityTokenValidators.Add(new GoogleTokenValidator(googleClientId));
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "StoryBooks.Api v1"));
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
        
    }
}