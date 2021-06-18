using BooksStoreKhaled.Models;
using BooksStoreKhaled.Models.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BooksStoreKhaled
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(
                options => options.EnableEndpointRouting = false
            );
            services.AddSingleton<IBooksStoreRepository<Author>, cAuthorRepository>();
            services.AddSingleton<IBooksStoreRepository<Book>, cBookRepository>();
            services.AddDbContext<StoreBooksDbContext>(options=> {
                options.UseSqlServer(configuration.GetConnectionString("SqlConnection"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}
