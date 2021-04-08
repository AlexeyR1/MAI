using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using TrackingTasksProgressSystem.EFCore;

namespace TrackingTasksProgressSystem
{
    public class Startup
    {
        public IConfiguration Configuration { get; }


        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            #region Конфигурация контейнера зависимостей

            // Настройка контекста БД
            /* https://docs.microsoft.com/ru-ru/ef/core/dbcontext-configuration/#implicitly-sharing-dbcontext-instances-via-dependency-injection
             * 1). Transient - служба создается каждый раз, когда она запрашивается из контейнера
             * 2). Scoped (by default) - служба создается для каждого запроса (подключения) клиента
             */
            services.AddDbContext<TrackingTasksProgressDbContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));

            services.AddControllers();

            #endregion
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Компонент маршрутизации
            app.UseRouting();

            // Компонент обработки кросс-доменных запросов
            app.UseCors(builder =>
            {
                builder.SetIsOriginAllowed(source => "http://localhost:3000" == source).AllowAnyMethod().AllowAnyHeader();
            });

            // Установка компонентов контроллера в качестве конечных точек маршрута обработки запроса (внутри контроллера - маршрутизация на основе атрибутов)
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
