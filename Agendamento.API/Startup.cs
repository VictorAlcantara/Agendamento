using Agendamento.API.Data;
using Agendamento.API.Domain.Interfaces;
using Agendamento.API.Domain.Interfaces.Repositories;
using Agendamento.API.Domain.Interfaces.Services;
using Agendamento.API.Domain.Services;
using Agendamento.API.Filters;
using Agendamento.API.Infra.Repositories;
using Agendamento.API.Options;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Agendamento.API
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
            services.AddDbContextPool<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddMvc(options => options.Filters.Add<ValidationFilter>())
                .AddFluentValidation(fluentValidationConfig => fluentValidationConfig.RegisterValidatorsFromAssemblyContaining<Startup>())
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSwaggerGen(options =>
                options.SwaggerDoc("v1", new Info { Title = "AgendamentoAPI", Version = "v1" })
            );

            services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
            services.AddScoped<ISalaRepository, SalaRepository>();
            services.AddScoped<IAgendaRepository, AgendaRepository>();
            services.AddScoped(typeof(IServiceBase<>), typeof(ServiceBase<>));
            services.AddScoped<ISalaService, SalaService>();
            services.AddScoped<IAgendaService, AgendaService>();

            services.AddAutoMapper(typeof(Startup));

            services.AddCors(options =>
            {
                options.AddPolicy("EnableCORS", builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials().Build();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("EnableCORS");

            app.UseMvc();

            var swaggerOptions = new SwaggerOptionsDTO();
            Configuration.GetSection("SwaggerOptions").Bind(swaggerOptions);

            app.UseSwagger();
            app.UseSwaggerUI(setup =>
                setup.SwaggerEndpoint(swaggerOptions.UIEndpoint, swaggerOptions.Description));
        }
    }
}
