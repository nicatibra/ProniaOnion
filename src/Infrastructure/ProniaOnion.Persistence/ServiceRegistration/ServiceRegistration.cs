using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProniaOnion.Application.Abstractions.Repositories;
using ProniaOnion.Application.Abstractions.Services;
using ProniaOnion.Domain.Entities;
using ProniaOnion.Persistence.Contexts;
using ProniaOnion.Persistence.Implementations.Repositories;
using ProniaOnion.Persistence.Implementations.Services;
using System.Reflection;

namespace ProniaOnion.Persistence.ServiceRegistration
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(opt =>
                opt.UseSqlServer(configuration.GetConnectionString("MSSQL"),
                //Avtomatiok Persistence-de migration olmasi ucun
                migration => migration.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName)
                ));

            services.AddIdentity<AppUser, IdentityRole>(opt =>
            {
                opt.Password.RequiredLength = 8;
                opt.Password.RequireNonAlphanumeric = false;

                opt.User.RequireUniqueEmail = true;

                opt.Lockout.AllowedForNewUsers = true;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
                opt.Lockout.MaxFailedAccessAttempts = 3;
            }
            ).AddDefaultTokenProviders().AddEntityFrameworkStores<AppDbContext>();


            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IColorRepository, ColorRepository>();
            services.AddScoped<ISizeRepository, SizeRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IBlogRepository, BlogRepository>();
            services.AddScoped<IGenreRepository, GenreRepository>();

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IColorService, ColorService>();
            services.AddScoped<ISizeService, SizeService>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IBlogService, BlogService>();
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();


            return services;
        }
    }
}
