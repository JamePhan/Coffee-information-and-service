using Back.Utilities;
using Library;
using Library.Models;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddCors();

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<CoffeehouseSystemContext>(
            option => option.UseSqlServer(
                builder.Configuration.GetConnectionString("CoffeehouseCS")
                )
            );

        builder.Services.AddAutoMapper(typeof(MapperProfile));

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseMiddleware<AntiXSSMiddleWare>();

        app.UseCors();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}