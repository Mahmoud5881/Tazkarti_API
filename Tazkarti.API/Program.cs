using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Tazkarti.Core.Models;
using Tazkarti.Core.RepositoryInterfaces;
using Tazkarti.Repository.Data;
using Tazkarti.Repository.Repositories;
using Tazkarti.Service.ServiceInterfaces;
using Tazkarti.Service.Services;
using AutoMapper;
using Tazkarti.API.Helpers;

namespace Tazkarti.API;

public class Program
{
    public static void Main(string[] args)
    {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            
            #region Services
            
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddIdentity<AppUser, IdentityRole>(options=>
                    options.Password.RequireNonAlphanumeric = false)
                .AddEntityFrameworkStores<AppDbContext>();
            
            builder.Services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
            builder.Services.AddScoped<IEventService,EventService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IMatchService, MatchService>();
            builder.Services.AddAutoMapper(cfg => { }, typeof(MappingProfile).Assembly);
            
            #endregion

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();

            app.UseAuthorization();
            
            app.MapControllers();

            app.Run();
        }
}